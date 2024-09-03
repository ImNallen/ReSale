using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;
using ReSale.Domain.Shared;

namespace ReSale.Application.Customers.GetById;

public class GetCustomerByIdQueryHandler(
    IReSaleDbContext context,
    IMapper mapper)
    : IQueryHandler<GetCustomerByIdQuery, CustomerResult>
{
    public async Task<Result<CustomerResult>> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        CustomerResult? customer = await context.Customers
            .Where(x => x.Id == request.Id)
            .Select(c => mapper.Map<CustomerResult>(c))
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerResult>(DomainErrors.NotFound(nameof(Customer)));
        }

        return customer;
    }
}
