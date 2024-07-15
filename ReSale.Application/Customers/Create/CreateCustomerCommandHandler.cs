using MapsterMapper;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : ICommandHandler<CreateCustomerCommand, CustomerResult>
{
    public async Task<Result<CustomerResult>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        
        if (emailResult.IsFailure)
        {
            return Result.Failure<CustomerResult>(emailResult.Error);
        }

        if (firstNameResult.IsFailure)
        {
            return Result.Failure<CustomerResult>(firstNameResult.Error);
        }
        
        if (lastNameResult.IsFailure)
        {
            return Result.Failure<CustomerResult>(lastNameResult.Error);
        }
        
        if (phoneNumberResult.IsFailure)
        {
            return Result.Failure<CustomerResult>(phoneNumberResult.Error);
        }
        
        var isEmailUnique = await unitOfWork.Customers.IsEmailUniqueAsync(emailResult.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<CustomerResult>(EmailErrors.NotUnique);
        }
        
        var customer = Customer.Create(
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value,
            new Address(
                request.Street, 
                request.City, 
                request.ZipCode, 
                request.Country, 
                request.State),
            phoneNumberResult.Value);
        
        await unitOfWork.Customers.AddAsync(customer, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<CustomerResult>(customer);
    }
}