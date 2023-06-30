using System.Net;
using System.Text.Json;
using Application;
using Domain;
using Persistence;

namespace TreeNodeApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public ExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();

            await _next(context);
        }
        catch (Exception ex)
        {
            string requestBodyString;
            context.Request.Body.Position = 0;

            using (var sr = new StreamReader(context.Request.Body))
            {
                requestBodyString = await sr.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            var journal = new Journal
            {
                EventId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                QueryParameters = context.Request.QueryString.ToString(),
                BodyParameters = requestBodyString,
                StackTrace = ex.StackTrace
            };

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TreeNodeDbContext>();

                dbContext.Journals.Add(journal);
                await dbContext.SaveChangesAsync();
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new SecureExceptionResponse
            {
                Type = ex is SecureException ? "Secure" : "Exception",
                EventId = journal.EventId.ToString(),
                Message = ex is SecureException ? ex.Message : $"Internal server error ID = {journal.EventId}",
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}