using ReSale.Domain.Users;

namespace ReSale.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken);
}