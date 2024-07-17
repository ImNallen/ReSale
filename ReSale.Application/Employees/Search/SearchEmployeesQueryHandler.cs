using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Employees.Search;

public class SearchEmployeesQueryHandler(
    IUnitOfWork unitOfWork) 
    : IQueryHandler<SearchEmployeesQuery, PagedList<EmployeeResult>>
{
    public async Task<Result<PagedList<EmployeeResult>>> Handle(
        SearchEmployeesQuery request, 
        CancellationToken cancellationToken)
    {
        var employees = await unitOfWork.Employees
            .SearchEmployeesAsync(
                request.SearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.Page,
                request.PageSize,
                cancellationToken);
        
        return employees;
    }
}