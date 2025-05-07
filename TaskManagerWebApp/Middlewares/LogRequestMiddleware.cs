namespace TaskManagerWebApp.Middlewares;

public class LogRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogRequestMiddleware> _logger;

    public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

        // Appel du prochain middleware dans le pipeline
        await _next(context);

        _logger.LogInformation($"Response: {context.Response.StatusCode}");
    }
}
