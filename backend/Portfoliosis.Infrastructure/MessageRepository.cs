using Microsoft.EntityFrameworkCore;
using Portfoliosis.Core.Entities;
using Portfoliosis.Core.Repositories;

namespace Portfoliosis.Infrastructure;

public class MessageRepository : EfRepositoryBase, IMessageRepository
{
    public async Task<Message?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Message>().FirstOrDefaultAsync(message => message.Id == id, cancellationToken);
    }

    public void Add(Message message)
    {
        _dbContext.Set<Message>().Add(message);
    }

    public void Update(Message message)
    {
        _dbContext.Set<Message>().Update(message);
    }


    public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
