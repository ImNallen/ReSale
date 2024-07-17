using Refit;
using ReSale.Api.Contracts.Responses.Employees;
using ReSale.Web.Models;

namespace ReSale.Web.Clients;

public interface IEmployeesClient
{
    [Get("/api/v1/employees/search?searchTerm={searchTerm}&page={page}&pageSize={pageSize}")]
    Task<PagedList<EmployeeResponse>> SearchEmployees(
        [Query] string? searchTerm,
        [Query] int page,
        [Query] int pageSize);
}