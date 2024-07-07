using ReSale.Domain.Common;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Employees;

public sealed class Employee : Entity
{
    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    
    private Employee(
        Email email,
        FirstName firstName,
        LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    private Employee()
    {
    }

    public static Employee Create(
        Email email,
        FirstName firstName,
        LastName lastName)
    {
        var employee = new Employee(email, firstName, lastName);
        
        // TODO: Add domain events here
        
        return employee;
    }
}