using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Application.Employees.Delete;

public class DeleteEmployeeCommandHandler(
    IReSaleDbContext context)
    : ICommandHandler<DeleteEmployeeCommand, bool>
{
    public async Task<Result<bool>> Handle(
        DeleteEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        Employee? employee = await context.Employees
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (employee is null)
        {
            return Result.Failure<bool>(DomainErrors.NotFound(nameof(Employee)));
        }

        context.Employees.Remove(employee);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
