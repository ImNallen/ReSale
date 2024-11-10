using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Activities;
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

        var description = new Description($"Customer '{customer.Id}' deleted");

        var activity = Activity.Create(description, ActivityType.CustomerDeleted);

        await context.Activities.AddAsync(activity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
