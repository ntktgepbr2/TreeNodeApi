﻿namespace Domain;

public class Journal
{
    public int Id { get; set; }
    public Guid EventId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? QueryParameters { get; set; }
    public string? BodyParameters { get; set; }
    public string? StackTrace { get; set; }
}