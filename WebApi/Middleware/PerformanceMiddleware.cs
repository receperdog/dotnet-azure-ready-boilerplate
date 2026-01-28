using System;
using System.Diagnostics;

namespace WebApi.Middleware;

public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;
    private readonly long _slowRequestThresholdMs;

    public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger, IConfiguration configuration)
    {
        _next = next;
        _logger = logger;
        _slowRequestThresholdMs = configuration.GetValue<long>("Performance:SlowRequestThresholdMs", 500);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            if (elapsedMs > _slowRequestThresholdMs)
            {
                // if req is slow log as warn
                _logger.LogWarning(
                    "Slow request detected. Method: {Method}, Path: {Path}, Duration: {Duration}ms, StatusCode: {StatusCode}, TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    elapsedMs,
                    context.Response.StatusCode,
                    context.TraceIdentifier);
            }
        }
    }
}
