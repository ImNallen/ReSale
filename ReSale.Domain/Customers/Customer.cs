using ReSale.Domain.Common;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Customers;

public sealed class Customer : Entity
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Address Address { get; private set; }
    
    private Customer(
        Guid id, 
        FirstName firstName, 
        LastName lastName, 
        Address address)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
    }
    
    public static Customer Create(FirstName firstName, LastName lastName, Address address)
    {
        var customer = new Customer(
            Guid.NewGuid(),
            firstName,
            lastName,
            address);

        return customer;
    }
}