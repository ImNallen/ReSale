using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Persistence.Repositories;
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
    
    public ReSaleDbContext ReSaleDbContext => Context;
}