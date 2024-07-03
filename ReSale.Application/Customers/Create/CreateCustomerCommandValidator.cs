using FluentValidation;

namespace ReSale.Application.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.Street)
            .NotEmpty();
        
        RuleFor(x => x.City)
            .NotEmpty();
        
        RuleFor(x => x.Country)
            .NotEmpty();
        
        RuleFor(x => x.ZipCode)
            .NotEmpty();
    }
}