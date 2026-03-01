using Domain.Entities;

namespace Domain.Interfaces;

public interface IQueuedEmailRepository : IRepositoryBase<QueuedEmail, int>
{
    Task SaveEmailQueueAsync(QueuedEmail queuedEmail);
    Task<QueuedEmail> GetQueuedEmailByIdAsync(int Id);
    Task<IList<QueuedEmail>> GetQueuedEmailsByStatusAsync(QueuedEmailStatus queuedEmailStatus);
}
