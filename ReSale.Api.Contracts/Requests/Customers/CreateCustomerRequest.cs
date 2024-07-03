namespace ReSale.Api.Contracts.Requests.Customers;

public record CreateCustomerRequest(
    string Email,
    string FirstName,
    string LastName,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country);