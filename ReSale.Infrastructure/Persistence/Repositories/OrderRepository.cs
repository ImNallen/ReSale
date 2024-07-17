using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Orders;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ReSaleDbContext context) 
        : base(context)
    {
    }
    
    public ReSaleDbContext ReSaleDbContext => Context;
}