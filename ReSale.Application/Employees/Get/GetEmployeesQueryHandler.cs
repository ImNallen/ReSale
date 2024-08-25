using System.Globalization;
using System.Linq.Expressions;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;

namespace ReSale.Application.Employees.Get;

public class GetEmployeesQueryHandler(
    IReSaleDbContext context,
    IMapper mapper)
    : IQueryHandler<GetEmployeesQuery, PagedList<EmployeeResult>>
{
    public async Task<Result<PagedList<EmployeeResult>>> Handle(
        GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Employee> employeesQuery = context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            employeesQuery = employeesQuery
                .Where(c =>
                    ((string)c.Email).Contains(request.SearchTerm) ||
                    ((string)c.FirstName).Contains(request.SearchTerm) ||
                    ((string)c.LastName).Contains(request.SearchTerm));
        }

        Expression<Func<Employee, object>> keySelector = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) switch
        {
            "email" => c => c.Email,
            "firstName" => c => c.FirstName,
            "lastName" => c => c.LastName,
            _ => c => c.Id
        };

        employeesQuery = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) == "desc"
            ? employeesQuery.OrderByDescending(keySelector)
            : employeesQuery.OrderBy(keySelector);

        int totalCount = await employeesQuery.CountAsync(cancellationToken);

        List<EmployeeResult> employees = await employeesQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(e => mapper.Map<EmployeeResult>(e))
            .ToListAsync(cancellationToken);

        return PagedList<EmployeeResult>.Create(employees, request.Page, request.PageSize, totalCount);
    }
}
