using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;

namespace ReSale.Application.Customers.Get;

public class GetCustomersQueryHandler(
    ICustomerRepository customerRepository)
    : IQueryHandler<GetCustomersQuery, PagedList<CustomerResponse>>
{
    public async Task<Result<PagedList<CustomerResponse>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await customerRepository
            .GetCustomersAsync(
                request.SearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.Page,
                request.PageSize,
                cancellationToken);

        return customers;
    }
}