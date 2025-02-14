using Microsoft.EntityFrameworkCore;
using Portfoliosis.Core.Repositories;

namespace Portfoliosis.Infrastructure;

public class EfRepositoryBase : IRepository
{
    protected readonly ApplicationDbContext _dbContext;

    public EfRepositoryBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}

