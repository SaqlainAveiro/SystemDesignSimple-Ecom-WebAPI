namespace Domain.Entities;

public class OrderItem : IEntity<int>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int PriceAtPurchase { get; set; }
}
