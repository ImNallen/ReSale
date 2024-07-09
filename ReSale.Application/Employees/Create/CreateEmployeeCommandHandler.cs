using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Application.Employees.Create;

public class CreateEmployeeCommandHandler(
    IUnitOfWork unitOfWork) 
    : ICommandHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateEmployeeCommand request, 
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        
        if (email.IsFailure)
        {
            return Result.Failure<Guid>(email.Error);
        }
        
        var isEmailUnique = await unitOfWork.Employees.IsEmailUniqueAsync(email.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<Guid>(EmailErrors.NotUnique);
        }

        var employee = Employee.Create(
            email.Value,
            new FirstName(request.FirstName),
            new LastName(request.LastName));
        
        await unitOfWork.Employees.AddAsync(employee, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return employee.Id;
    }
}