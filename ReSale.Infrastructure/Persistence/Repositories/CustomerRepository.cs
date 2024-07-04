using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;
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

    public async Task<CustomerResponse?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Customers
            .Where(x => x.Id == id)
            .Select(c => new CustomerResponse(
                c.Id,
                c.Email.Value,
                c.FirstName.Value,
                c.LastName.Value,
                c.Address.Street,
                c.Address.City,
                c.Address.ZipCode,
                c.Address.Country,
                c.Address.State))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedList<CustomerResponse>> GetCustomersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var customersQuery = context.Customers.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            customersQuery = customersQuery
                .Where(c => 
                    ((string)c.Email).Contains(searchTerm) ||
                    ((string)c.FirstName).Contains(searchTerm) ||
                    ((string)c.LastName).Contains(searchTerm));
        }
        
        Expression<Func<Customer, object>> keySelector = sortColumn?.ToLower() switch
        {
            "email" => c => c.Email,
            "firstName" => c => c.FirstName,
            "lastName" => c => c.LastName,
            "country" => c => c.Address.Country,
            _ => c => c.Id
        };

        customersQuery = sortOrder?.ToLower() == "desc" 
            ? customersQuery.OrderByDescending(keySelector) 
            : customersQuery.OrderBy(keySelector);

        var totalCount = await customersQuery.CountAsync(cancellationToken);
        
        var customers = await customersQuery
            .Skip((page - 1) * pageSize) 
            .Take(pageSize)
            .Select(c => new CustomerResponse(
                c.Id,
                c.Email.Value,
                c.FirstName.Value,
                c.LastName.Value,
                c.Address.Street,
                c.Address.City,
                c.Address.ZipCode,
                c.Address.Country,
                c.Address.State))
            .ToListAsync(cancellationToken);

        return PagedList<CustomerResponse>.Create(customers, page, pageSize, totalCount);
    }

    public Task<CustomerResponse?> GetCustomerByEmailAsync(
        string email, 
        CancellationToken cancellationToken)
    {
        return context.Customers
            .Where(c => ((string)c.Email) == email)
            .Select(c => new CustomerResponse(
                c.Id,
                c.Email.Value,
                c.FirstName.Value,
                c.LastName.Value,
                c.Address.Street,
                c.Address.City,
                c.Address.ZipCode,
                c.Address.Country,
                c.Address.State))
            .SingleOrDefaultAsync(cancellationToken);
    }
}