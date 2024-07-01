using MediatR;
using ReSale.Domain.Common;

namespace ReSale.Application.Abstractions.Messaging;

public interface ITransactionalCommand : ICommand;

public interface ITransactionalCommand<TResponse> : IRequest<Result<TResponse>>, ITransactionalCommand;
