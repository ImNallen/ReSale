using System.Data;
using ReSale.Application.Abstractions.Persistence.Repositories;

namespace ReSale.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ICustomerRepository Customers { get; }
    IEmployeeRepository Employees { get; }
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IOrderDetailRepository OrderDetails { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}