namespace WebAppDemo.Middlewares;

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
        _logger.LogInformation($"[{typeof(LogRequestMiddleware).Name}] Request: {context.Request.Method} {context.Request.Path}");

        //Appel du middleware suivant
        await _next(context);

        _logger.LogInformation($"[{typeof(LogRequestMiddleware).Name}] Status: {context.Response.StatusCode}");
    }
}
