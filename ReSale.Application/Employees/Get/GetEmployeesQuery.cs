using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Employees.Results;

namespace ReSale.Application.Employees.Get;

public class GetEmployeesQuery : ICachedQuery<List<EmployeeResult>>
{
    public string CacheKey => $"employees";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}