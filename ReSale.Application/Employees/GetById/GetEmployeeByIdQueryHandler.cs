using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;

namespace ReSale.Application.Employees.GetById;

public class GetEmployeeByIdQueryHandler(
    IReSaleDbContext context,
    IMapper mapper)
    : IQueryHandler<GetEmployeeByIdQuery, EmployeeResult>
{
    public async Task<Result<EmployeeResult>> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        EmployeeResult? employee = await context.Employees
            .Where(x => x.Id == request.Id)
            .Select(c => mapper.Map<EmployeeResult>(c))
            .FirstOrDefaultAsync(cancellationToken);

        if (employee is null)
        {
            return Result.Failure<EmployeeResult>(EmployeeErrors.NotFound);
        }

        return employee;
    }
}
