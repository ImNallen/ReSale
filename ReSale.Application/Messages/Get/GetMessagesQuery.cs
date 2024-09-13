using System;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Messages.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Messages.Get;

public record GetMessagesQuery(
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize)
    : IQuery<PagedList<MessageResult>>;
