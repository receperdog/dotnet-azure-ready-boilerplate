using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware pipeline.
app.UseGlobalExceptionHandler();


app.MapGet("/", () => "Hello World!");

// Test endpoints
app.MapGet("/error", (HttpContext context) => throw new Exception("Test exception"));
app.MapGet("/notfound", (HttpContext context) => throw new KeyNotFoundException("Item not found"));
app.MapGet("/badrequest", (HttpContext context) => throw new ArgumentException("Invalid input"));

app.Run();
