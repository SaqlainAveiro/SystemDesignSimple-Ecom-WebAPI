namespace Domain.Entities;

public class IdempotencyKeyRecord : IEntity<int>
{
    public int Id { get; set; }
    public string Key { get; set; }
    public DateTime CreatedOn { get; set; }
    public int OrderId { get; set; }
}
