using Domain.Entities;
using Domain.Interfaces;

namespace Application;

public interface IEComUnitOfWork : IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    IQueuedEmailRepository QueuedEmailRepository { get; }
    IIdempotencyKeyRecordRepository IdempotencyKeyRecordRepository { get; }
    Task SaveOrderItemsAsync(List<OrderItem> orderItems);
    Task<object> BeginTransactionAsync();
}
