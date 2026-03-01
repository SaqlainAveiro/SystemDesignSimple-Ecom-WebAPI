namespace Domain.Entities;

public class Order : IEntity<int>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Status { get; set; }
}
