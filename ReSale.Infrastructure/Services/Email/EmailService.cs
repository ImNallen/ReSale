using FluentEmail.Core;
using FluentEmail.Smtp;
using ReSale.Application.Abstractions.Services;

namespace ReSale.Infrastructure.Services.Email;

public class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    public async Task SendAsync(string to, string subject, string body) => await fluentEmail
            .To(to)
            .Subject(subject)
            .Body(body, isHtml: true)
            .SendAsync();
}
