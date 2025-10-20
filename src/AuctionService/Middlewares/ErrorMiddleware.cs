using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace AuctionService.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private static readonly JsonSerializerOptions _options = new() 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.UnprocessableEntity, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode code, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var error = new
            {
                status = (int)code,
                message = ex.Message,
            };

            var json = JsonSerializer.Serialize(error, _options);
            await context.Response.WriteAsync(json);
        }
    }

    public class NotFoundException(string message) : Exception(message){}

    public class BadRequestException(string message) : Exception(message){}
}
