﻿using BookifyApplication.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors;
public class LoggingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;
        try
        {
            _logger.LogInformation($"Processing {name} command ");
            var result = await next();
            _logger.LogInformation($"Processed {name} command successfully");
            return result;

        }
        catch (Exception exception)
        {
            _logger.LogError($"Error processing {name} command: {exception.Message}");
            throw;
        }


    }
}
