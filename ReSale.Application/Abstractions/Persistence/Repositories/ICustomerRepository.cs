using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Abstractions.Persistence.Repositories;

public interface ICustomerRepository
{
    Task<bool> IsEmailUniqueAsync(Email email);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
}