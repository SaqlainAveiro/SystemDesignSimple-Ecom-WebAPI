using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
