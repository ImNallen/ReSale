using System;

namespace ReSale.Application.Messages.Results;

public record MessageResult(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedAt);
