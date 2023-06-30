namespace Application.Commands;

public record GetJournalRangeCommand(int Skip, int Take, DateTime FilterFrom, DateTime FilterTo);