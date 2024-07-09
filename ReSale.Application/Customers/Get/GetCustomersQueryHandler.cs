using MapsterMapper;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Customers.Get;

public class GetCustomersQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IQueryHandler<GetCustomersQuery, List<CustomerResult>>
{
    public async Task<Result<List<CustomerResult>>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.Customers.GetAllAsync(cancellationToken);
        
        return customers.Select(mapper.Map<CustomerResult>).ToList();
    }
}