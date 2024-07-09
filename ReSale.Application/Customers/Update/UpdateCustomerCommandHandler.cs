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
        
        customer.Update(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Address(
                request.Street,
                request.City,
                request.ZipCode,
                request.Country,
                request.State));
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<CustomerResult>(customer);
    }
}