using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IdempotencyKeyRecordRepository : Repository<IdempotencyKeyRecord, int>, IIdempotencyKeyRecordRepository
{
    private readonly EComDbContext _eComDbContext;

    public IdempotencyKeyRecordRepository(EComDbContext eComDbContext) : base(eComDbContext)
    {
        _eComDbContext = eComDbContext;
    }

    public async Task<IdempotencyKeyRecord> GetIdempotencyKeyRecordByKeyAsync(string key)
    {
        return await _eComDbContext.IdempotencyKeyRecords
            .FirstOrDefaultAsync(x => x.Key == key);
    }
}
