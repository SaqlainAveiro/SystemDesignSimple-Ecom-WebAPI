using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class QueuedEmailRepository : Repository<QueuedEmail, int>, IQueuedEmailRepository
{
    private readonly EComDbContext _eComDbContext;

    public QueuedEmailRepository(EComDbContext eComDbContext) : base(eComDbContext)
    {
        _eComDbContext = eComDbContext;
    }

    public async Task<QueuedEmail> GetQueuedEmailByIdAsync(int Id)
    {
        return await _eComDbContext.QueuedEmails.FindAsync(Id);
    }

    public async Task SaveEmailQueueAsync(QueuedEmail queuedEmail)
    {
        await _eComDbContext.QueuedEmails.AddAsync(queuedEmail);
    }

    public async Task<IList<QueuedEmail>> GetQueuedEmailsByStatusAsync(QueuedEmailStatus queuedEmailStatus)
    {
        return await _eComDbContext.QueuedEmails
            .Where(x => x.EmailStatus == (int)queuedEmailStatus)
            .Take(10)
            .ToListAsync();
    }
}
