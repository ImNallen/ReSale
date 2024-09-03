using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.Delete;

public class DeleteCustomerCommandHandler(
    IReSaleDbContext context)
    : ICommandHandler<DeleteCustomerCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        Customer? customer = await context.Customers.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            return Result.Failure<bool>(DomainErrors.NotFound(nameof(Customer)));
        }

        context.Customers.Remove(customer);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
