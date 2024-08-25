using ReSale.Domain.Common;
using ReSale.Domain.Orders;
using ReSale.Domain.Products;
using ReSale.Domain.Shared;

namespace ReSale.Domain.OrderDetails;

public class OrderDetail : Entity
{
    private OrderDetail(Guid id, Guid orderId, Guid productId, Quantity quantity, Money price)
        : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private OrderDetail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public Quantity Quantity { get; private set; }

    public Money Price { get; private set; }

    // Navigation properties
    public virtual Order Order { get; private set; } = null!;

    public virtual Product Product { get; private set; } = null!;

    public static OrderDetail Create(Guid id, Guid orderId, Guid productId, Quantity quantity, Money price) => new OrderDetail(id, orderId, productId, quantity, price);
}
