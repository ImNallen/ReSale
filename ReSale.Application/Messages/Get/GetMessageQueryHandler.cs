using System;
using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Messages.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Messages;

namespace ReSale.Application.Messages.Get;

public class GetMessageQueryHandler(
    IReSaleDbContext context)
: IQueryHandler<GetMessagesQuery, PagedList<MessageResult>>
{
    public async Task<Result<PagedList<MessageResult>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Message> messagesQuery = context.Messages.AsQueryable();

        Expression<Func<Message, object>> keySelector = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) switch
        {
            "title" => m => m.Title,
            "content" => m => m.Content,
            "createdat" => m => m.CreatedAt,
            _ => m => m.Id
        };

        messagesQuery = request.SortOrder?.ToLower(CultureInfo.CurrentCulture) == "desc"
            ? messagesQuery.OrderByDescending(keySelector)
            : messagesQuery.OrderBy(keySelector);

        int totalCount = await messagesQuery.CountAsync(cancellationToken);

        List<MessageResult> messages = await messagesQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(m => new MessageResult(
                m.Id,
                m.Title.Value,
                m.Content.Value,
                m.CreatedAt))
            .ToListAsync(cancellationToken);

        return PagedList<MessageResult>.Create(messages, request.Page, request.PageSize, totalCount);
    }
}
