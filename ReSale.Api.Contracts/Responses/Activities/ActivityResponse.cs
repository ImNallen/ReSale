namespace ReSale.Api.Contracts.Responses.Activities;

public record ActivityResponse(
    Guid Id,
    string Description,
    string Type,
    DateTime CreatedAt);
