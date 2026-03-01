using Domain.Entities;

namespace Domain.Interfaces;

public interface IOrderRepository : IRepositoryBase<Order, int>
{
    Task<Order> GetOrderAsync(int orderId);
}
