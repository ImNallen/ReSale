using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReSale.Domain.Products;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasConversion(p => p.Value, 
                v => new Name(v))
            .HasColumnName(nameof(Name))
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Description)
            .HasConversion(p => p.Value, 
                v => new Description(v))
            .HasColumnName(nameof(Description))
            .HasMaxLength(1000)
            .IsRequired();
        
        builder.OwnsOne(b => b.Price, priceBuilder => priceBuilder
            .Property(m => m.Currency)
            .HasConversion(c => c.Code, code => Currency.FromCode(code))
            .HasColumnName("Price")
            .IsRequired());

        builder.HasMany(p => p.OrderDetails)
            .WithOne(o => o.Product)
            .HasForeignKey(o => o.ProductId);
    }
}