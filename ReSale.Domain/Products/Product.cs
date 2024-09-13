using ReSale.Domain.Common;
using ReSale.Domain.OrderDetails;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Products;

public class Product : Entity
{
    private Product(Guid id, Name name, Description description, Money price)
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public Money Price { get; private set; }

    // Navigation properties
    public virtual ICollection<OrderDetail> OrderDetails { get; init; } = [];

    public static Product Create(Name name, Description description, Money price) => new Product(Guid.NewGuid(), name, description, price);
}
