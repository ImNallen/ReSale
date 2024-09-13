using System;

namespace ReSale.Api.Contracts.Responses.Messages;

public record MessageResponse(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt);
