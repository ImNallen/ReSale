using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReSale.Domain.OrderDetails;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(od => od.Id);

        builder.Property(od => od.Quantity)
            .HasConversion(
                od => od.Value,
                v => new Quantity(v))
            .HasColumnName(nameof(OrderDetail.Quantity))
            .IsRequired();

        builder.OwnsOne(b => b.Price, priceBuilder => priceBuilder
            .Property(m => m.Currency)
            .HasConversion(c => c.Code, code => Currency.FromCode(code))
            .HasColumnName("Price")
            .IsRequired());

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        builder.HasOne(od => od.Product)
            .WithMany(b => b.OrderDetails)
            .HasForeignKey(od => od.ProductId);
    }
}
