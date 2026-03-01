using Domain.Entities;

namespace Domain.Interfaces;

public interface IIdempotencyKeyRecordRepository : IRepositoryBase<IdempotencyKeyRecord, int>
{
    Task<IdempotencyKeyRecord> GetIdempotencyKeyRecordByKeyAsync(string key);
}
