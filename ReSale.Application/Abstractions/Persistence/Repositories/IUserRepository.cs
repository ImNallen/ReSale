using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email email);
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
}