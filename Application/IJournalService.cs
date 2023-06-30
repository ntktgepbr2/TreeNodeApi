using Domain;

namespace Application;

public interface IJournalService
{
    public Task<Journal> Get(int id);
    public Task<PaginatedJournals> GetRange(int skip, int take, DateTime filterFrom, DateTime filterTo);
}