using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfoliosis.Core.Entities;
using Portfoliosis.Core.ValueObjects;

namespace Portfoliosis.Infrastructure.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.Name)
            .HasMaxLength(Message.MaxNameLength);
        builder.Property(m => m.Email)
            .HasConversion(v => v.Value, v => MyEmailAddress.TryCreate(v).Value)
            .HasMaxLength(MyEmailAddress.MaxTotalLength);
        builder.Property(m => m.Text)
            .HasMaxLength(Message.MaxTextLength);
    }
}
