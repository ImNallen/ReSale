using ReSale.Domain.Common;

namespace ReSale.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(User User) : IDomainEvent;
