using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReSale.Domain.Activities;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Description)
            .HasConversion(
                d => d.Value,
                v => new Description(v))
            .HasColumnName(nameof(Description))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder.Property(a => a.Type)
            .HasConversion(
                v => v.ToString(),
                v => (ActivityType)Enum.Parse(typeof(ActivityType), v))
            .HasColumnName("Type")
            .HasMaxLength(50)
            .IsRequired();
    }
}
