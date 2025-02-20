using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Portfoliosis.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReferencer.Assembly);
}
