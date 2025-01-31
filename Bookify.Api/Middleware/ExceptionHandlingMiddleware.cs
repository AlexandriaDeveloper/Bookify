using Bookify.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;


namespace Bookify.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"An error occurred while processing a request. {ex.InnerException}");
            var exceptionDetails = GetExceptionDetails(ex);
            var problemDetails = new ProblemDetails
            {
                Title =exceptionDetails.Title,
                Status = exceptionDetails.Status,
                Detail = exceptionDetails.Detail,
                Type = exceptionDetails.Type,
            };
            if (exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"]= exceptionDetails.Errors;
            }
            context.Response.StatusCode = (int)exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception ex) => ex switch
    {
        ValidationException validationException => new ExceptionDetails(
            StatusCodes.Status400BadRequest,
            "ValidationFailure",
            "Validation Error",
            "One or more Validation errors has occurred",
            validationException.Errors),
        _ => new ExceptionDetails(
        StatusCodes.Status500InternalServerError,
        "Server Error",
"InternalServerError",
        "An unexpected error occurred on the server",
        null)
    };

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}
