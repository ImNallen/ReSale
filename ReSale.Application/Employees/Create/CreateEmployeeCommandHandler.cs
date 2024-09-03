using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Employees;
using ReSale.Domain.Shared;

namespace ReSale.Application.Employees.Create;

public class CreateEmployeeCommandHandler(
    IReSaleDbContext context,
    IMapper mapper)
    : ICommandHandler<CreateEmployeeCommand, EmployeeResult>
{
    public async Task<Result<EmployeeResult>> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);

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

        bool emailExists = await context.Employees
            .AnyAsync(x => x.Email == emailResult.Value, cancellationToken);

        if (emailExists)
        {
            return Result.Failure<EmployeeResult>(DomainErrors.NotUnique(nameof(Email)));
        }

        var employee = Employee.Create(
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value,
            request.HireDate,
            new Address(
                request.Street,
                request.City,
                request.ZipCode,
                request.Country,
                request.State),
            new Money(
                request.Amount,
                Currency.FromCode(request.Currency)));

        await context.Employees.AddAsync(employee, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<EmployeeResult>(employee);
    }
}
