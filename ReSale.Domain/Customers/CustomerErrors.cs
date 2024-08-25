using ReSale.Domain.Common;

namespace ReSale.Domain.Customers;

public static class CustomerErrors
{
    public static readonly Error NotFound = Error.NotFound("Customer.NotFound", "Customer not found.");
}
