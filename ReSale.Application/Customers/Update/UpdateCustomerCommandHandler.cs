using MapsterMapper;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.Update;

public class UpdateCustomerCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : ICommandHandler<UpdateCustomerCommand, CustomerResult>
{
    public async Task<Result<CustomerResult>> Handle(
        UpdateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers
            .GetAsync(request.Id, cancellationToken);
        
        if (customer is null) 
            return Result.Failure<CustomerResult>(CustomerErrors.NotFound);
        
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        
        if (firstNameResult.IsFailure)
        {
            return Result.Failure<CustomerResult>(firstNameResult.Error);
        }
        
        if (lastNameResult.IsFailure)
        {
            return Result.Failure<CustomerResult>(lastNameResult.Error);
        }

        Address? billingAddress = null;
        if (request.BillingStreet is not null &&
            request.BillingCity is not null &&
            request.BillingZipCode is not null &&
            request.BillingCountry is not null &&
            request.BillingState is not null)
        {
            billingAddress = new Address(
                request.BillingStreet,
                request.BillingCity,
                request.BillingZipCode,
                request.BillingCountry,
                request.BillingState);
        }
        
        customer.Update(
            firstNameResult.Value,
            lastNameResult.Value,
            new Address(
                request.ShippingStreet,
                request.ShippingCity,
                request.ShippingZipCode,
                request.ShippingCountry,
                request.ShippingState),
            billingAddress);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<CustomerResult>(customer);
    }
}