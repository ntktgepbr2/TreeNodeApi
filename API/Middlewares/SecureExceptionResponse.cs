namespace TreeNodeApi.Middlewares;

/// <summary>
/// Secure exception response.
/// </summary>
public class SecureExceptionResponse
{
    /// <summary>
    /// Exception type.
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Event id.
    /// </summary>
    public string EventId { get; set; }
    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; set; }
}