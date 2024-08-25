using ReSale.Domain.Common;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Employees;

public sealed class Employee : Entity
{
    private Employee(
        Email email,
        FirstName firstName,
        LastName lastName,
        DateOnly hireDate,
        Address address,
        Money salary)
    {
        FirstName = firstName;
        LastName = lastName;
        HireDate = hireDate;
        Email = email;
        Address = address;
        Salary = salary;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Employee()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Email Email { get; private set; }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Address Address { get; private set; }

    public DateOnly HireDate { get; private set; }

    public Money Salary { get; private set; }

    public static Employee Create(
        Email email,
        FirstName firstName,
        LastName lastName,
        DateOnly hireDate,
        Address address,
        Money salary)
    {
        var employee = new Employee(email, firstName, lastName, hireDate, address, salary);

        return employee;
    }
}
