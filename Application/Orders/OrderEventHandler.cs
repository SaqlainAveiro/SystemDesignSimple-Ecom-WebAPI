using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Events;

namespace Application.Orders;

public class OrderEventHandler : IEventHandler<OrderCreatedEvent>
{
    private readonly IEComUnitOfWork _eComUnitOfWork;

    public OrderEventHandler(IEComUnitOfWork eComUnitOfWork)
    {
        _eComUnitOfWork = eComUnitOfWork;
    }

    public async Task HandleEventAsync(OrderCreatedEvent eventMessage)
    {
        var newQueuedEmail = new QueuedEmail()
        {
            OrderId = eventMessage.OrderId,
            RetryCount = 1,
            Message = $"Order {eventMessage.OrderId} created by Customer {eventMessage.CustomerEmail}",
            EmailStatus = (int)QueuedEmailStatus.Pending
        };
        await _eComUnitOfWork.QueuedEmailRepository.SaveEmailQueueAsync(newQueuedEmail);
    }
}
