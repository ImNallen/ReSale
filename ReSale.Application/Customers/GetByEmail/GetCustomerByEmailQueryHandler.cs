using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.GetByEmail;

public class GetCustomerByEmailQueryHandler(
    ICustomerRepository customerRepository)
    : IQueryHandler<GetCustomerByEmailQuery, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository
            .GetCustomerByEmailAsync(
                request.Email, 
                cancellationToken);
        
        if (customer is null)
        {
            return Result.Failure<CustomerResponse>(CustomerErrors.NotFound);
        }
        
        return customer;
    }
}