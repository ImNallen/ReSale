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
        var email = Email.Create(request.Email);
        
        if (email.IsFailure)
        {
            return Result.Failure<CustomerResult>(email.Error);
        }
        
        var isEmailUnique = await unitOfWork.Customers.IsEmailUniqueAsync(email.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<CustomerResult>(EmailErrors.NotUnique);
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
        
        return mapper.Map<CustomerResult>(customer);
    }
}