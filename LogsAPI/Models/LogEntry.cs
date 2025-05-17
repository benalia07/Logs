namespace LogsAPI.Models;

public class LogEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int ActionId { get; set; }
    public Guid EntityId { get; set; }
    public string? Message { get; set; }
    public DateTime Date { get; set; }
}
