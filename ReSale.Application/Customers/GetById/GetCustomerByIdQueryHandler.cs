using MapsterMapper;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Customers;

namespace ReSale.Application.Customers.GetById;

public class GetCustomerByIdQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
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

        return mapper.Map<CustomerResult>(customer);
    }
}