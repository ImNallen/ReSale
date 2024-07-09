using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Application.Employees.Create;

public class CreateEmployeeCommandHandler(
    IUnitOfWork unitOfWork) 
    : ICommandHandler<CreateEmployeeCommand, EmployeeResult>
{
    public async Task<Result<EmployeeResult>> Handle(
        CreateEmployeeCommand request, 
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        
        if (email.IsFailure)
        {
            return Result.Failure<EmployeeResult>(email.Error);
        }
        
        var isEmailUnique = await unitOfWork.Employees.IsEmailUniqueAsync(email.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<EmployeeResult>(EmailErrors.NotUnique);
        }

        var employee = Employee.Create(
            email.Value,
            new FirstName(request.FirstName),
            new LastName(request.LastName));
        
        await unitOfWork.Employees.AddAsync(employee, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new EmployeeResult(
            employee.Id,
            employee.Email.Value,
            employee.FirstName.Value,
            employee.LastName.Value);
    }
}