using Refit;
using ReSale.Api.Contracts.Requests.Employees;
using ReSale.Api.Contracts.Responses.Employees;
using ReSale.Web.Models;

namespace ReSale.Web.Clients;

public interface IEmployeesClient
{
    [Get("/api/v1/employees?searchTerm={searchTerm}&page={page}&pageSize={pageSize}")]
    Task<PagedList<EmployeeResponse>> Get(
        [Query] string? searchTerm,
        [Query] int page,
        [Query] int pageSize);

    [Get("/api/v1/employees/{id}")]
    Task<EmployeeResponse?> GetById(Guid id);

    [Post("/api/v1/employees")]
    Task<EmployeeResponse> Create(CreateEmployeeRequest request);

    [Delete("/api/v1/employees/{id}")]
    Task Delete(Guid id);
}
