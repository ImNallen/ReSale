using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Auth.Verify;

public record VerifyCommand(Guid Token) : ICommand;
