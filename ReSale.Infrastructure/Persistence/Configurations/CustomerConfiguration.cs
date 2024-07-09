﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
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

        builder.OwnsOne(c => c.Address);
        
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}