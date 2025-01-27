using Bookify.Domain.Abstractions;
using MediatR;

namespace BookifyApplication.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
