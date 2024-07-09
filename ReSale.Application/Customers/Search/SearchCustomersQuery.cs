using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Customers.Shared;
using ReSale.Domain.Common;

namespace ReSale.Application.Customers.Search;

public sealed record SearchCustomersQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) 
    : IQuery<PagedList<CustomerResponse>>;