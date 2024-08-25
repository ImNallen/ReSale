using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Employees.Results;

namespace ReSale.Application.Employees.GetById;

public record GetEmployeeByIdQuery(Guid Id) : ICachedQuery<EmployeeResult>
{
    public string CacheKey => $"employee-by-id-{Id}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(0.2);
}
