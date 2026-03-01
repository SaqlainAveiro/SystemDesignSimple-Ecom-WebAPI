using Domain.Entities;

namespace Application.Orders;

public interface IQueuedEmailService
{
    Task<QueuedEmail> GetQueuedEmailByIdAsync(int Id);
    Task SaveEmailQueueAsync(QueuedEmail queuedEmail);
}
