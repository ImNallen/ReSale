using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Messages;

namespace ReSale.Application.Messages.Create;

public class CreateMessageCommandHandler(IReSaleDbContext context) : ICommandHandler<CreateMessageCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var title = new Title(request.Title);
        var content = new Content(request.Content);

        var message = Message.Create(title, content);

        context.Messages.Add(message);

        await context.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
