using Domain.Entities;

namespace Application.Orders;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order, List<OrderItem> orderItems, string idempotencyKeyHeader);
}
