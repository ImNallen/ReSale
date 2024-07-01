using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Auth.Token;

public record TokenCommand(string Email, string Password) : ICommand<TokenResponse>;