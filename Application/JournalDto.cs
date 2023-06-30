namespace Application;

public class JournalDto
{
    public int Id { get; set; }
    public Guid EventId { get; set; }

    public DateTime CreatedAt { get; set; }
}