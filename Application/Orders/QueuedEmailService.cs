using Domain.Entities;

namespace Application.Orders;

public class QueuedEmailService : IQueuedEmailService
{
    private readonly IEComUnitOfWork _eComUnitOfWork;

    public QueuedEmailService(IEComUnitOfWork eComUnitOfWork)
    {
        _eComUnitOfWork = eComUnitOfWork;
    }

    public async Task<QueuedEmail> GetQueuedEmailByIdAsync(int Id)
    {
        return await _eComUnitOfWork.QueuedEmailRepository.GetQueuedEmailByIdAsync(Id);
    }

    public async Task SaveEmailQueueAsync(QueuedEmail queuedEmail)
    {
        await _eComUnitOfWork.QueuedEmailRepository.SaveEmailQueueAsync(queuedEmail);
    }
}
