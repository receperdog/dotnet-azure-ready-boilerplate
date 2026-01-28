using System;
using System.Net;

namespace WebApi.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call next middleware
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred. TraceId: {TraceId}", context.TraceIdentifier);
            await HandleExceptionAsync(context, ex);
        }
    }

    public static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // response type will be JSON
        context.Response.ContentType = "application/json";

        var statusCode = GetStatusCode(exception);

        var message = GetErrorMessage(exception);

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = message,
            TraceId = context.TraceIdentifier // For monitoring purpose
        };

        await context.Response.WriteAsJsonAsync(response);
    }

    private static HttpStatusCode GetStatusCode(Exception exception) => exception switch
    {
        ArgumentException => HttpStatusCode.BadRequest,
        UnauthorizedAccessException => HttpStatusCode.Unauthorized,
        KeyNotFoundException => HttpStatusCode.NotFound,
        _ => HttpStatusCode.InternalServerError
    };

    private static string GetErrorMessage(Exception exception) => exception switch
    {
        ArgumentException => exception.Message,
        UnauthorizedAccessException => "Unauthorized access",
        KeyNotFoundException => "Resource not found",
        _ => "An error occurred"
    };
}
