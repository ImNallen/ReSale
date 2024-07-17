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

        RuleFor(x => x.ShippingStreet)
            .NotEmpty();
        
        RuleFor(x => x.ShippingCity)
            .NotEmpty();
        
        RuleFor(x => x.ShippingCountry)
            .NotEmpty();
        
        RuleFor(x => x.ShippingZipCode)
            .NotEmpty();
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
    }
}