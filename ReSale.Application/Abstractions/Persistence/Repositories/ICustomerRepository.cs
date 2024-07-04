using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Abstractions.Persistence.Repositories;

public interface ICustomerRepository
{
    Task<bool> IsEmailUniqueAsync(Email email);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task<CustomerResponse?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedList<CustomerResponse>> GetCustomersAsync(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<CustomerResponse?> GetCustomerByEmailAsync(
        string email, 
        CancellationToken cancellationToken);
}