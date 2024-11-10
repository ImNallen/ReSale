namespace ReSale.Application.Activities.Results;

public record ActivityResult(
    Guid Id,
    string Description,
    string Type,
    DateTime CreatedAt);
