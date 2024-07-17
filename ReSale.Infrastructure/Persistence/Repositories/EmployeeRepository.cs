using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Infrastructure.Persistence.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ReSaleDbContext context) 
        : base(context)
    {
    }
    
    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await Context.Employees.AnyAsync(u => u.Email == email);
    }
    
    public async Task<PagedList<EmployeeResult>> SearchEmployeesAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var employeesQuery = Context.Employees.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            employeesQuery = employeesQuery
                .Where(c => 
                    ((string)c.Email).Contains(searchTerm) ||
                    ((string)c.FirstName).Contains(searchTerm) ||
                    ((string)c.LastName).Contains(searchTerm));
        }
        
        Expression<Func<Employee, object>> keySelector = sortColumn?.ToLower() switch
        {
            "email" => c => c.Email,
            "firstName" => c => c.FirstName,
            "lastName" => c => c.LastName,
            _ => c => c.Id
        };

        employeesQuery = sortOrder?.ToLower() == "desc" 
            ? employeesQuery.OrderByDescending(keySelector) 
            : employeesQuery.OrderBy(keySelector);

        var totalCount = await employeesQuery.CountAsync(cancellationToken);
        
        var customers = await employeesQuery
            .Skip((page - 1) * pageSize) 
            .Take(pageSize)
            .Select(c => new EmployeeResult(
                c.Id,
                c.Email.Value,
                c.FirstName.Value,
                c.LastName.Value))
            .ToListAsync(cancellationToken);

        return PagedList<EmployeeResult>.Create(customers, page, pageSize, totalCount);
    }
    
    public ReSaleDbContext ReSaleDbContext => Context;
}