using ReSale.Domain.Common;

namespace ReSale.Domain.Messages;

public sealed class Message : Entity
{
    private Message(
        Guid id,
        Title title,
        Content content,
        DateTime createdAt)
        : base(id)
    {
        Title = title;
        Content = content;
        CreatedAt = createdAt;
    }

    public Title Title { get; private set; }

    public Content Content { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public static Message Create(Title title, Content content)
    {
        var message = new Message(
            Guid.NewGuid(),
            title,
            content,
            DateTime.UtcNow);

        return message;
    }
}
