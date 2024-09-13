using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Messages.Create;

public record CreateMessageCommand(string Title, string Content) : ICommand<Guid>;
