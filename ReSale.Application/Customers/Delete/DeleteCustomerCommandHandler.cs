using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.Delete;

public class DeleteCustomerCommandHandler(
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCustomerCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = await unitOfWork
            .Customers.GetAsync(
            request.Id, 
            cancellationToken);
        
        if (customer is null)
            return Result.Failure<bool>(CustomerErrors.NotFound);
        
        unitOfWork.Customers.Remove(customer);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}