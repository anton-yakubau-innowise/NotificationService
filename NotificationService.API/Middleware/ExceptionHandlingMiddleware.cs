using System.Net;
using System.Text.Json;

namespace NotificationService.API.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "An internal server error has occurred.";

            switch (exception)
            {
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case ArgumentException:
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
    
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}