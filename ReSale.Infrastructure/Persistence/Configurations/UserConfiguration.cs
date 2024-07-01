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
        
        builder.Property(u => u.Password)
            .HasConversion(e => e.Value,
                v => Password.Create(v).Value)
            .HasColumnName(nameof(Password))
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(u => u.FirstName)
            .HasConversion(e => e.Value,
                v => new FirstName(v))
            .HasColumnName(nameof(FirstName))
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(u => u.LastName)
            .HasConversion(e => e.Value,
                v => new LastName(v))
            .HasColumnName(nameof(LastName))
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}