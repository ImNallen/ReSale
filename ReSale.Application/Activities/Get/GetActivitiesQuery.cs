using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Activities.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Activities.Get;

public record GetActivitiesQuery(
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<ActivityResult>>;
