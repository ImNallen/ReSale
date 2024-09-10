using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.Get;

public class GetCustomersQueryHandler(
    IReSaleDbContext context)
    : IQueryHandler<GetCustomersQuery, PagedList<CustomerResult>>
{
    public async Task<Result<PagedList<CustomerResult>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Customer> customersQuery = context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searchTermCapitalized = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.SearchTerm);
            customersQuery = customersQuery
                .Where(c =>
                    ((string)c.Email).Contains(request.SearchTerm) ||
                    ((string)c.Email).Contains(searchTermCapitalized) ||
                    ((string)c.FirstName).Contains(request.SearchTerm) ||
                    ((string)c.FirstName).Contains(searchTermCapitalized) ||
                    ((string)c.LastName).Contains(request.SearchTerm) ||
                    ((string)c.LastName).Contains(searchTermCapitalized));
        }

        Expression<Func<Customer, object>> keySelector = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) switch
        {
            "email" => c => c.Email,
            "firstName" => c => c.FirstName,
            "lastName" => c => c.LastName,
            _ => c => c.Id
        };

        customersQuery = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) == "desc"
            ? customersQuery.OrderByDescending(keySelector)
            : customersQuery.OrderBy(keySelector);

        int totalCount = await customersQuery.CountAsync(cancellationToken);

        List<CustomerResult> customers = await customersQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new CustomerResult(
                c.Id,
                c.Email.Value,
                c.FirstName.Value,
                c.LastName.Value,
                c.ShippingAddress.Street,
                c.ShippingAddress.City,
                c.ShippingAddress.ZipCode,
                c.ShippingAddress.Country,
                c.ShippingAddress.State!,
                c.PhoneNumber.Value,
                c.BillingAddress!.Street,
                c.BillingAddress.City,
                c.BillingAddress.ZipCode,
                c.BillingAddress.Country,
                c.BillingAddress.State))
            .ToListAsync(cancellationToken);

        return PagedList<CustomerResult>.Create(customers, request.Page, request.PageSize, totalCount);
    }
}
