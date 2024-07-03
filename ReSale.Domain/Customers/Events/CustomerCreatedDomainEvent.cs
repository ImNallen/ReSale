using ReSale.Domain.Common;

namespace ReSale.Domain.Customers.Events;

public record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;