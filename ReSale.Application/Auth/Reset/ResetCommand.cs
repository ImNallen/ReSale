using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Auth.Reset;

public record ResetCommand(Guid Token, string Password) : ICommand;
