using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Employees.Get;

public class GetEmployeesQueryHandler(
    IReSaleDbContext context,
    IMapper mapper) 
    : IQueryHandler<GetEmployeesQuery, List<EmployeeResult>>
{
    public async Task<Result<List<EmployeeResult>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await context.Employees.ToListAsync(cancellationToken);

        return employees.Select(mapper.Map<EmployeeResult>).ToList();
    }
}