using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Employees.Get;

public class GetEmployeesQueryHandler(
    IUnitOfWork unitOfWork) 
    : IQueryHandler<GetEmployeesQuery, List<EmployeeResult>>
{
    public async Task<Result<List<EmployeeResult>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await unitOfWork.Employees.GetAllAsync(cancellationToken);

        return employees.Select(e => new EmployeeResult(
            e.Id,
            e.Email.Value,
            e.FirstName.Value,
            e.LastName.Value)).ToList();
    }
}