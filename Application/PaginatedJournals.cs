namespace Application;

public class PaginatedJournals
{
    public int Skip { get; set; }
    public int Count { get; set; }

    public IReadOnlyCollection<JournalDto> Items { get; set; } = new List<JournalDto>();
}