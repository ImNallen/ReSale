using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.GetById;

public class GetCustomerByIdQueryHandler(
    ICustomerRepository repository) 
    : IQueryHandler<GetCustomerByIdQuery, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerResponse>(CustomerErrors.NotFound);
        }

        return customer;
    }
}