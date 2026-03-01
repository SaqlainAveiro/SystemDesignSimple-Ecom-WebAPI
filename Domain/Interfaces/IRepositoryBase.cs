using Domain.Entities;

namespace Domain.Interfaces;

public interface IRepositoryBase<TEntity, TKey> where TEntity : class, 
    IEntity<TKey> where TKey : IComparable
{
    Task AddAsync(TEntity entity);
    Task EditAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(TKey id);
    Task<IList<TEntity>> GetAllAsync();
}
