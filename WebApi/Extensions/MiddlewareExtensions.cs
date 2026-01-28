using System;

namespace WebApi.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<Middleware.GlobalExceptionMiddleware>();
    }

    public static IApplicationBuilder UsePerformanceMonitoring(this IApplicationBuilder app)
    {
        return app.UseMiddleware<Middleware.PerformanceMiddleware>();
    }

}
