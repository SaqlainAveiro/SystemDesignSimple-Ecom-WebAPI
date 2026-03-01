namespace Infrastructure.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public string CustomerEmail { get; set; }
}

