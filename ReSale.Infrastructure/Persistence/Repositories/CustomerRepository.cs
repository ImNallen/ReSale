using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(
        ReSaleDbContext context) 
        : base(context)
    {
    }
    
    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await Context.Customers.AnyAsync(u => u.Email == email);
    }

    public async Task<CustomerResult?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Customers
            .Where(x => x.Id == id)
            .Select(c => new CustomerResult(
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

    public async Task<PagedList<CustomerResult>> SearchCustomersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var customersQuery = Context.Customers.AsQueryable();
        
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
            .Select(c => new CustomerResult(
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

        return PagedList<CustomerResult>.Create(customers, page, pageSize, totalCount);
    }

    public Task<CustomerResult?> GetCustomerByEmailAsync(
        string email, 
        CancellationToken cancellationToken)
    {
        return Context.Customers
            .Where(c => ((string)c.Email) == email)
            .Select(c => new CustomerResult(
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
    
    public ReSaleDbContext ReSaleDbContext => Context;
}