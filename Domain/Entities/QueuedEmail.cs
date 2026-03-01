namespace Domain.Entities;

public class QueuedEmail : IEntity<int>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int RetryCount { get; set; }
    public string Message { get; set; }
    public int EmailStatus { get; set; }
}
