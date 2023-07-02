namespace TreeNodeApi.Contracts;

/// <summary>
/// Filter by date.
/// </summary>
/// <param name="From">From date</param>
/// <param name="To">To date</param>
public record Filter(DateTime From,DateTime To);