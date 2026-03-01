using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public interface IRepository<TEntity, TKey> : IRepositoryBase<TEntity, TKey> 
    where TEntity : class, IEntity<TKey>
    where TKey : IComparable
{
    Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
        bool isTrackingOff = false);
}
