using ReSale.Domain.Users;

namespace ReSale.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}