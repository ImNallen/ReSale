using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Auth.Forgot;

public record ForgotPasswordCommand(string Email) : ICommand;
