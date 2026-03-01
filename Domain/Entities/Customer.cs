namespace Domain.Entities;

public class Customer : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
