using ReSale.Domain.Common;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Activities;

public class Activity : Entity
{
    private Activity(Guid id, Description description, DateTime createdAt, ActivityType type)
        : base(id)
    {
        Description = description;
        CreatedAt = createdAt;
        Type = type;
    }

    public Description Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ActivityType Type { get; private set; }

    public static Activity Create(Description description, ActivityType type) => new Activity(Guid.NewGuid(), description, DateTime.UtcNow, type);
}
