using ReSale.Application.Auth.Results;
using ReSale.Domain.Users;

namespace ReSale.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    AccessTokenResult GenerateToken(User user);
}
