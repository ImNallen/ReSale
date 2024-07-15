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
        var emailResult = Email.Create(request.Email);
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        
        if (emailResult.IsFailure)
        {
            return Result.Failure<EmployeeResult>(emailResult.Error);
        }

        if (firstNameResult.IsFailure)
        {
            return Result.Failure<EmployeeResult>(firstNameResult.Error);
        }
        
        if (lastNameResult.IsFailure)
        {
            return Result.Failure<EmployeeResult>(lastNameResult.Error);
        }
        
        var isEmailUnique = await unitOfWork.Employees.IsEmailUniqueAsync(emailResult.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<EmployeeResult>(EmailErrors.NotUnique);
        }

        var employee = Employee.Create(
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value);
        
        await unitOfWork.Employees.AddAsync(employee, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new EmployeeResult(
            employee.Id,
            employee.Email.Value,
            employee.FirstName.Value,
            employee.LastName.Value);
    }
}