using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.GetByEmail;

public class GetCustomerByEmailQueryHandler(
    IReSaleDbContext context,
    IMapper mapper)
    : IQueryHandler<GetCustomerByEmailQuery, CustomerResult>
{
    public async Task<Result<CustomerResult>> Handle(
        GetCustomerByEmailQuery request,
        CancellationToken cancellationToken)
    {
        CustomerResult? customer = await context.Customers
            .Where(c => (string)c.Email == request.Email)
            .Select(c => mapper.Map<CustomerResult>(c))
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerResult>(DomainErrors.NotFound(nameof(Customer)));
        }

        return customer;
    }
}
