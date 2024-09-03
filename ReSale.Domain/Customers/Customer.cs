using ReSale.Domain.Common;
using ReSale.Domain.Customers.Events;
using ReSale.Domain.Orders;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Customers;

public sealed class Customer : Entity
{
    private Customer(
        Guid id,
        Email email,
        FirstName firstName,
        LastName lastName,
        Address shippingAddress,
        Address? billingAddress,
        PhoneNumber phoneNumber)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        PhoneNumber = phoneNumber;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Customer()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Address ShippingAddress { get; private set; }

    public Address? BillingAddress { get; private set; }

    // Navigation properties
    public ICollection<Order> Orders { get; private set; } = [];

    public static Customer Create(
        Email email,
        FirstName firstName,
        LastName lastName,
        Address shippingAddress,
        Address? billingAddress,
        PhoneNumber phoneNumber)
    {
        var customer = new Customer(
            Guid.NewGuid(),
            email,
            firstName,
            lastName,
            shippingAddress,
            billingAddress,
            phoneNumber);

        customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(customer.Id));

        return customer;
    }

    public Customer Update(
        FirstName firstName,
        LastName lastName,
        Address shippingAddress,
        Address? billingAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;

        return this;
    }
}
