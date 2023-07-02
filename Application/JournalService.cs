using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application;

public class JournalService : IJournalService
{
    private readonly TreeNodeDbContext _context;
    private readonly IMapper _mapper;

    public JournalService(TreeNodeDbContext _context, IMapper mapper)
    {
        this._context = _context;
        _mapper = mapper;
    }
    public async Task<Journal> Get(int id)
    {
      return await _context.Journals.FindAsync(id) ??
             throw new SecureException($"There is no journal with id {id}");
    }

    public async Task<PaginatedJournals> GetRange(int skip, int take, DateTime filterFrom, DateTime filterTo)
    {
        var query = _context.Journals
            .Where(j => j.CreatedAt >= filterFrom && j.CreatedAt <= filterTo);

        var totalCount = await query.CountAsync();

        var journals = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return new PaginatedJournals
        {
            Skip = skip,
            Count = totalCount,
            Items = _mapper.Map<IReadOnlyCollection<JournalDto>>(journals)
        };
    }
}