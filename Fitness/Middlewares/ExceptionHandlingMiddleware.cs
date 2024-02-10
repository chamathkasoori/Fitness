using Fitness.Core.Common;
using System.Net;

namespace Fitness.Middlewares;
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unexpected error occurred.");

        //ErrorDetails response = exception switch
        //{
        //    ApplicationException _ => new ErrorDetails(HttpStatusCode.BadRequest, "Application exception occurred."),
        //    KeyNotFoundException _ => new ErrorDetails(HttpStatusCode.NotFound, "The request key not found."),
        //    UnauthorizedAccessException _ => new ErrorDetails(HttpStatusCode.Unauthorized, "Unauthorized."),
        //    _ => new ErrorDetails(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        //};
        ErrorDetails response = new ErrorDetails(HttpStatusCode.BadRequest, exception.ToString());

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}
