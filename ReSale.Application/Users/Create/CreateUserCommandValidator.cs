using FluentValidation;

namespace ReSale.Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .MaximumLength(200)
            .EmailAddress();

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(c => c.FirstName)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .Length(2, 100);
    }
}