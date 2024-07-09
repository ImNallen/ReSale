using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.GetByEmail;

public class GetCustomerByEmailQueryHandler(
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetCustomerByEmailQuery, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers
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