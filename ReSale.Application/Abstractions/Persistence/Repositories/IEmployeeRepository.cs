using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Application.Abstractions.Persistence.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<bool> IsEmailUniqueAsync(Email email);
}