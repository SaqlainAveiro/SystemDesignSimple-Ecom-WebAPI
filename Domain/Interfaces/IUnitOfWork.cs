namespace Domain.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    Task SaveChangesAsync();
}
