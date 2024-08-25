using Refit;
using ReSale.Api.Contracts.Requests.Customers;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Web.Models;

namespace ReSale.Web.Clients;

public interface ICustomersClient
{
    [Get("/api/v1/customers?searchTerm={searchTerm}&page={page}&pageSize={pageSize}")]
    Task<PagedList<CustomerResponse>> Get(
        [Query] string? searchTerm,
        [Query] int page,
        [Query] int pageSize);

    [Get("/api/v1/customers/{id}")]
    Task<CustomerResponse?> GetById(Guid id);

    [Post("/api/v1/customers")]
    Task<CustomerResponse> Create(CreateCustomerRequest customerRequest);

    [Put("/api/v1/customers/{id}")]
    Task<CustomerResponse> Update(Guid id, UpdateCustomerRequest customerRequest);

    [Delete("/api/v1/customers/{id}")]
    Task Delete(Guid id);
}
