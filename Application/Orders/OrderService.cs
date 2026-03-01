using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Events;
using System.Data.Common;

namespace Application.Orders;

public class OrderService : IOrderService
{
    private readonly IEComUnitOfWork _eComUnitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public OrderService(IEComUnitOfWork eComUnitOfWork,
        IEventPublisher eventPublisher)
    {
        _eComUnitOfWork = eComUnitOfWork;
        _eventPublisher = eventPublisher;
    }

    public async Task<Order> CreateOrderAsync(Order order,
        List<OrderItem> orderItems,
        string idempotencyKeyHeader)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        if (string.IsNullOrWhiteSpace(order.Status) ||
            order.CreatedOn > DateTime.Now)
        {
            return null; //not allowed with these values
        }

        var existingOrderKey = await
            _eComUnitOfWork
            .IdempotencyKeyRecordRepository
            .GetIdempotencyKeyRecordByKeyAsync(idempotencyKeyHeader);

        if (existingOrderKey != null)
        {
            var existingOrder = await _eComUnitOfWork.OrderRepository.GetByIdAsync(existingOrderKey.OrderId);
            return existingOrder;
        }

        using var dbTransaction = (DbTransaction)await _eComUnitOfWork.BeginTransactionAsync();

        await _eComUnitOfWork.OrderRepository.AddAsync(order);

        var newKey = new IdempotencyKeyRecord()
        {
            OrderId = order.Id,
            Key = idempotencyKeyHeader,
            CreatedOn = DateTime.Now
        };
        await _eComUnitOfWork
            .IdempotencyKeyRecordRepository.AddAsync(newKey);

        foreach (var item in orderItems)
        {
            item.OrderId = order.Id;
        }
        await _eComUnitOfWork.SaveOrderItemsAsync(orderItems);

        var orderCreated = new OrderCreatedEvent()
        {
            OrderId = order.Id,
            CustomerEmail = ""
        };
        await _eventPublisher.PublishAsync<OrderCreatedEvent>(orderCreated);

        await _eComUnitOfWork.SaveChangesAsync();
        await dbTransaction.CommitAsync();

        return order;
    }
}
