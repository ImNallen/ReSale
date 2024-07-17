using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.OrderDetails;

namespace ReSale.Domain.Orders;

public class Order : Entity
{
    public Guid CustomerId { get; private set; }
    
    // Navigation properties
    public virtual Customer Customer { get; private set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; private set; } = [];
    
    private Order(Guid id, Guid customerId)
        : base(id)
    {
        CustomerId = customerId;
    }

    private Order()
    {
    }
    
    public static Order Create(Guid id, Guid customerId)
    {
        return new Order(id, customerId);
    }
}