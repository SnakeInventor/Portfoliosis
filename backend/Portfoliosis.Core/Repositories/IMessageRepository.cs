using Portfoliosis.Core.Entities;

namespace Portfoliosis.Core.Repositories;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Message message);

    void Update(Message message);
}
