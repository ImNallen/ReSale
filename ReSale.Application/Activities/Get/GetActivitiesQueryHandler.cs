using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Activities.Results;
using ReSale.Domain.Activities;
using ReSale.Domain.Common;

namespace ReSale.Application.Activities.Get;

public class GetActivitiesQueryHandler(IReSaleDbContext context) : IQueryHandler<GetActivitiesQuery, PagedList<ActivityResult>>
{
    public async Task<Result<PagedList<ActivityResult>>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Activity> activitiesQuery = context.Activities.AsQueryable();

        Expression<Func<Activity, object>> keySelector = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) switch
        {
            "description" => a => a.Description,
            "createdat" => a => a.CreatedAt,
            _ => a => a.Id
        };

        activitiesQuery = request.SortOrder?.ToLower(CultureInfo.CurrentCulture) == "desc"
            ? activitiesQuery.OrderByDescending(keySelector)
            : activitiesQuery.OrderBy(keySelector);

        int totalCount = await activitiesQuery.CountAsync(cancellationToken);

        List<ActivityResult> activities = await activitiesQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(a => new ActivityResult(
                a.Id,
                a.Description.Value,
                a.Type.ToString(),
                a.CreatedAt))
            .ToListAsync(cancellationToken);

        return PagedList<ActivityResult>.Create(activities, request.Page, request.PageSize, totalCount);
    }
}
