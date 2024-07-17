using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Abstractions.Persistence.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<bool> IsEmailUniqueAsync(Email email);
    Task<CustomerResult?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedList<CustomerResult>> SearchCustomersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<CustomerResult?> GetCustomerByEmailAsync(
        string email, 
        CancellationToken cancellationToken);
}