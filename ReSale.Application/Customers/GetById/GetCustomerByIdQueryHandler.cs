using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.GetById;

public class GetCustomerByIdQueryHandler(
    IUnitOfWork unitOfWork) 
    : IQueryHandler<GetCustomerByIdQuery, CustomerResult>
{
    public async Task<Result<CustomerResult>> Handle(
        GetCustomerByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerResult>(CustomerErrors.NotFound);
        }

        return new CustomerResult(
            customer.Id,
            customer.Email.Value,
            customer.FirstName.Value,
            customer.LastName.Value,
            customer.Address.Street,
            customer.Address.City,
            customer.Address.ZipCode,
            customer.Address.Country,
            customer.Address.State);
    }
}