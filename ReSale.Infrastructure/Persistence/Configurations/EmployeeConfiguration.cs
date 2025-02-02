﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(u => u.Email)
            .HasConversion(
                e => e.Value,
                v => Email.Create(v).Value)
            .HasColumnName(nameof(Email))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasConversion(
                e => e.Value,
                v => FirstName.Create(v).Value)
            .HasColumnName(nameof(FirstName))
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasConversion(
                e => e.Value,
                v => LastName.Create(v).Value)
            .HasColumnName(nameof(LastName))
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.HireDate)
            .HasColumnName(nameof(Employee.HireDate))
            .IsRequired();

        builder.OwnsOne(b => b.Salary, priceBuilder => priceBuilder
            .Property(m => m.Currency)
            .HasConversion(c => c.Code, code => Currency.FromCode(code))
            .HasColumnName("Salary")
            .IsRequired());

        builder.OwnsOne(e => e.Address);

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
