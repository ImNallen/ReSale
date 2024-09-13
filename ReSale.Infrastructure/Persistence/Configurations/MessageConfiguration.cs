using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReSale.Domain.Messages;

namespace ReSale.Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .HasConversion(
                t => t.Value,
                v => new Title(v))
            .HasColumnName(nameof(Title))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(m => m.Content)
            .HasConversion(
                c => c.Value,
                v => new Content(v))
            .HasColumnName(nameof(Content))
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(m => m.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}
