using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Infrastructure.Persistence.Repositories;

namespace ReSale.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ReSaleDbContext _context;

    public UnitOfWork(ReSaleDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Customers = new CustomerRepository(_context);
        Employees = new EmployeeRepository(_context);
    }
    
    public IUserRepository Users { get; }
    public ICustomerRepository Customers { get; }
    public IEmployeeRepository Employees { get; }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        => await _context.SaveChangesAsync(cancellationToken);
}