using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.Delete;

public class DeleteCustomerCommandHandler(
    IReSaleDbContext context)
    : ICommandHandler<DeleteCustomerCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(request.Id, cancellationToken);
        
        if (customer is null)
            return Result.Failure<bool>(CustomerErrors.NotFound);
        
        context.Customers.Remove(customer);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}