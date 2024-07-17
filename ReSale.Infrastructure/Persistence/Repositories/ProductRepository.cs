using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Products;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ReSaleDbContext context) 
        : base(context)
    {
    }
    
    public ReSaleDbContext ReSaleDbContext => Context;
}