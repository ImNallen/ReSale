using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;

namespace ReSale.Application.Customers.Get;

public class GetCustomersQueryHandler(IUnitOfWork unitOfWork) 
    : IQueryHandler<GetCustomersQuery, List<CustomerResponse>>
{
    public async Task<Result<List<CustomerResponse>>> Handle(
        GetCustomersQuery request, 
        CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.Customers.GetAllAsync(cancellationToken);
        
        return customers.Select(c => new CustomerResponse
        (
            c.Id,
            c.Email.Value,
            c.FirstName.Value,
            c.LastName.Value,
            c.Address.Street,
            c.Address.City,
            c.Address.ZipCode,
            c.Address.Country,
            c.Address.State
        )).ToList();
    }
}