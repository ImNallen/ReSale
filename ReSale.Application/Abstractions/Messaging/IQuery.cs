using MediatR;
using ReSale.Domain.Common;

namespace ReSale.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
