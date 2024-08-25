using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Employees.Get;

public record GetEmployeesQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize)
    : IQuery<PagedList<EmployeeResult>>;
