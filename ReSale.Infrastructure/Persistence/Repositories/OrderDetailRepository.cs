using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.OrderDetails;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(ReSaleDbContext context) 
        : base(context)
    {
    }
    
    public ReSaleDbContext ReSaleDbContext => Context;
}