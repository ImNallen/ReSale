using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class CustomerRepository(ReSaleDbContext context) : ICustomerRepository
{
    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await context.Customers.AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(
        Customer customer, 
        CancellationToken cancellationToken = default)
    {
        await context.Customers.AddAsync(customer, cancellationToken);
    }
}