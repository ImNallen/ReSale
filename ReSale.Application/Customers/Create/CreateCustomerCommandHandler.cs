using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler(
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        
        if (email.IsFailure)
        {
            return Result.Failure<CustomerResponse>(email.Error);
        }
        
        var isEmailUnique = await unitOfWork.Customers.IsEmailUniqueAsync(email.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<CustomerResponse>(EmailErrors.NotUnique);
        }
        
        var customer = Customer.Create(
            email.Value,
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Address(
                request.Street, 
                request.City, 
                request.ZipCode, 
                request.Country, 
                request.State));
        
        await unitOfWork.Customers.AddAsync(customer, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CustomerResponse(
            customer.Id,
            customer.Email.Value,
            customer.FirstName.Value,
            customer.LastName.Value,
            customer.Address.Street,
            customer.Address.City,
            customer.Address.ZipCode,
            customer.Address.Country,
            customer.Address.State);
    }
}