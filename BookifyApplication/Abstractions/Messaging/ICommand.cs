﻿using Bookify.Domain.Abstractions;
using MediatR;

namespace BookifyApplication.Abstractions.Messaging;

public interface IBaseCommand { }

public interface ICommand : IRequest<Result>, IBaseCommand
{
}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}
