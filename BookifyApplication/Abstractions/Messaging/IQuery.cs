using Bookify.Domain.Abstractions;
using MediatR;

namespace BookifyApplication.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
