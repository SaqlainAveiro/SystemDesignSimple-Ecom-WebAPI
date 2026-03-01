using Application;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure;

public class EComUnitOfWork : UnitOfWork, IEComUnitOfWork
{
    private readonly EComDbContext _eComDbContext;

    public IOrderRepository OrderRepository { get; private set; }
    public IQueuedEmailRepository QueuedEmailRepository { get; private set; }
    public IIdempotencyKeyRecordRepository IdempotencyKeyRecordRepository { get; private set; }

    public EComUnitOfWork(EComDbContext eComDbContext,
        IOrderRepository orderRepository,
        IQueuedEmailRepository queuedEmailRepository,
        IIdempotencyKeyRecordRepository idempotencyKeyRecordRepository) : base(eComDbContext)
    {
        _eComDbContext = eComDbContext;
        OrderRepository = orderRepository;
        QueuedEmailRepository = queuedEmailRepository;
        IdempotencyKeyRecordRepository = idempotencyKeyRecordRepository;
    }

    public async Task SaveOrderItemsAsync(List<OrderItem> orderItems)
    {
        await _eComDbContext.AddRangeAsync(orderItems);
    }

    public async Task<object> BeginTransactionAsync()
    {
        return await _eComDbContext.Database.BeginTransactionAsync();
    }
}
