using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.GetByEmail;

public class GetCustomerByEmailQueryHandler(
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetCustomerByEmailQuery, CustomerResult>
{
    public async Task<Result<CustomerResult>> Handle(
        GetCustomerByEmailQuery request, 
        CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers
            .GetCustomerByEmailAsync(
                request.Email, 
                cancellationToken);
        
        if (customer is null)
        {
            return Result.Failure<CustomerResult>(CustomerErrors.NotFound);
        }
        
        return customer;
    }
}