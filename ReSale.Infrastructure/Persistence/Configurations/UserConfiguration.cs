using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasConversion(e => e.Value,
                v => Email.Create(v).Value)
            .HasColumnName(nameof(Email))
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(u => u.FirstName)
            .HasConversion(e => e.Value,
                v => FirstName.Create(v).Value)
            .HasColumnName(nameof(FirstName))
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(u => u.LastName)
            .HasConversion(e => e.Value,
                v => LastName.Create(v).Value)
            .HasColumnName(nameof(LastName))
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(user => user.IdentityId).IsUnique();
    }
}