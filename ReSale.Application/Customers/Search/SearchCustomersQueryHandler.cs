using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;

namespace ReSale.Application.Customers.Search;

public class SearchCustomersQueryHandler(
    IUnitOfWork unitOfWork)
    : IQueryHandler<SearchCustomersQuery, PagedList<CustomerResponse>>
{
    public async Task<Result<PagedList<CustomerResponse>>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.Customers
            .SearchCustomersAsync(
                request.SearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.Page,
                request.PageSize,
                cancellationToken);

        return customers;
    }
}