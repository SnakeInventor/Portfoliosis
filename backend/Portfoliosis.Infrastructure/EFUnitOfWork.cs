// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;

using Portfoliosis.Core.Repositories;

namespace Portfoliosis.Infrastructure;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public EfUnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
