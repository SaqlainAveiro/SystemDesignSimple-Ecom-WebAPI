using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IComparable
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public async Task EditAsync(TEntity entity)
    {
        await Task.Run(() =>
        {
            _dbContext.ChangeTracker.Clear();
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        });
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IList<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
        bool isTrackingOff = false)
    {
        throw new NotImplementedException();
    }
}
