using ReSale.Domain.Common;
using ReSale.Domain.Customers.Events;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Customers;

public sealed class Customer : Entity
{
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Address Address { get; private set; }
    
    private Customer(
        Guid id, 
        Email email,
        FirstName firstName, 
        LastName lastName, 
        Address address, 
        PhoneNumber phoneNumber)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    private Customer()
    {
    }
    
    public static Customer Create(
        Email email, 
        FirstName firstName, 
        LastName lastName, 
        Address address,
        PhoneNumber phoneNumber)
    {
        var customer = new Customer(
            Guid.NewGuid(),
            email,
            firstName,
            lastName,
            address,
            phoneNumber);
        
        customer.Raise(new CustomerCreatedDomainEvent(customer.Id));

        return customer;
    }

    public Customer Update(
        FirstName firstName,
        LastName lastName,
        Address address)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;

        return this;
    }
}