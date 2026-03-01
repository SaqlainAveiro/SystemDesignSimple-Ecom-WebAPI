using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class OrderRepository : Repository<Order, int>, IOrderRepository
{
    public OrderRepository(EComDbContext eComDbContext) : base(eComDbContext)
    {
        
    }

    public async Task<Order> GetOrderAsync(int orderId)
    {
        return await GetByIdAsync(orderId);
    }
}
