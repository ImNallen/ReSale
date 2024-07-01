using FluentValidation;

namespace ReSale.Application.Auth.Token;

public class TokenCommandValidator : AbstractValidator<TokenCommand>
{
    public TokenCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MinimumLength(5)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}