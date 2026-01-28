# Azure-Ready .NET Boilerplate

A production-ready ASP.NET Core 10.0 Web API boilerplate with enterprise-grade middleware and Azure best practices.

## Features

- ✅ **Global Exception Handling** - Centralized error handling with consistent JSON responses
- ✅ **Performance Monitoring** - Request duration tracking with slow request detection
- ✅ **Structured Logging** - Correlation tracking with TraceId for distributed systems
- ✅ **Azure Well-Architected Framework** - Follows reliability, performance, and operational excellence principles

## Project Structure

```
WebApi/
├── Middleware/
│   ├── GlobalExceptionMiddleware.cs
│   └── PerformanceMiddleware.cs
├── Extensions/
│   └── MiddlewareExtensions.cs
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

## Getting Started

```bash
cd WebApi
dotnet run
```

## Configuration

```json
{
  "Performance": {
    "SlowRequestThresholdMs": 500
  }
}
```

## Error Response Format

```json
{
  "statusCode": 500,
  "message": "An error occurred",
  "traceId": "00-abc123..."
}
```

## License

MIT
