using MaximTech.Domain.Models;

namespace MaximTech.WebApi.Middlewares;

public class RateLimiterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SemaphoreSlim _semaphore;

    public RateLimiterMiddleware(RequestDelegate next, Settings settings)
    {
        _next = next;
        _semaphore = new SemaphoreSlim(settings.ParallelLimit, settings.ParallelLimit);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var acquired = await _semaphore.WaitAsync(0);

        if (!acquired)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            return;
        }

        try
        {
            await _next(context);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}