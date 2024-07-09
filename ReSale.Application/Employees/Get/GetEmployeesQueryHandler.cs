using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Shared;
using ReSale.Domain.Common;

namespace ReSale.Application.Employees.Get;

public class GetEmployeesQueryHandler(
    IUnitOfWork unitOfWork) 
    : IQueryHandler<GetEmployeesQuery, List<EmployeeResponse>>
{
    public async Task<Result<List<EmployeeResponse>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await unitOfWork.Employees.GetAllAsync(cancellationToken);

        return employees.Select(e => new EmployeeResponse(
            e.Id,
            e.Email.Value,
            e.FirstName.Value,
            e.LastName.Value)).ToList();
    }
}