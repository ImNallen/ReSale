using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Employees.Shared;

namespace ReSale.Application.Employees.Get;

public class GetEmployeesQuery : ICachedQuery<List<EmployeeResponse>>
{
    public string CacheKey => $"employees";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}