using ReSale.Application.Abstractions.Messaging;
using ReSale.Domain.Common;

namespace ReSale.Application.Auth.Token;

public class TokenCommandHandler : ICommandHandler<TokenCommand, TokenResponse>
{
    public Task<Result<TokenResponse>> Handle(
        TokenCommand request, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}