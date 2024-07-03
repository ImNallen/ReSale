using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        
        if (email.IsFailure)
        {
            return Result.Failure<Guid>(email.Error);
        }
        
        var isEmailUnique = await customerRepository.IsEmailUniqueAsync(email.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<Guid>(EmailErrors.NotUnique);
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
        
        await customerRepository.AddAsync(customer, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return customer.Id;
    }
}