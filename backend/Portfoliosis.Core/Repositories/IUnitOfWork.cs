namespace Portfoliosis.Core.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Performs transaction, fires events if successful
    /// </summary>
    public Task CommitAsync(CancellationToken cancellationToken = default);
}
