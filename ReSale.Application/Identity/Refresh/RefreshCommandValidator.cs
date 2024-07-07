using FluentValidation;

namespace ReSale.Application.Users.Refresh;

public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}