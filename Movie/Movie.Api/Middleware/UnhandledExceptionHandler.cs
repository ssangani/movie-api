using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Movie.Api.Middleware
{
    public class UnhandledExceptionHandler : IMiddleware
    {
        private readonly ILogger _logger;

        public UnhandledExceptionHandler(
            ILogger<UnhandledExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                try
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(e, new JsonSerializerOptions { WriteIndented = true }));
                }
                catch
                {
                    // intentional no-op
                }
            }
        }
    }
}
