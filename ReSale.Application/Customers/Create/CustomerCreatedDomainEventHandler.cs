using MediatR;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Activities;
using ReSale.Domain.Customers.Events;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.Create;

internal sealed class CustomerCreatedDomainEventHandler(
    IReSaleDbContext context)
    : INotificationHandler<CustomerCreatedDomainEvent>
{
    public async Task Handle(
        CustomerCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var description = new Description($"Customer '{notification.CustomerId}' created");

        var activity = Activity.Create(description, ActivityType.CustomerCreated);

        await context.Activities.AddAsync(activity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}
