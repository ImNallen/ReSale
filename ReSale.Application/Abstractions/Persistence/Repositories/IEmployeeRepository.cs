using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Application.Abstractions.Persistence.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<bool> IsEmailUniqueAsync(Email email);
    Task<PagedList<EmployeeResult>> SearchEmployeesAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}