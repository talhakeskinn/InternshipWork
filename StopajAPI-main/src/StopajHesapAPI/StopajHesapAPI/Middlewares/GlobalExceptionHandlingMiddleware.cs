using StopajHesapAPI.Models;
using System;
using System.Net;
using System.Text.Json;

namespace StopajHesapAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            var response = context.Response;
            response.ContentType = "application/json";

            var errorDetails = new ErrorDetails
            {
                Success = false
            };

            switch (ex)
            {
                case UnauthorizedAccessException _:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                    errorDetails.Message = "Kimlik doğrulanmadı";
                    break;

                case ApplicationException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                    errorDetails.Message = "Geçersiz veya Eksik parametre";
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                    errorDetails.Message = "An unexpected error occurred";
                    break;
            }
            // Hata mesajını logla
            _logger.LogError(ex, ex.Message);

            var result = JsonSerializer.Serialize(errorDetails);
            await context.Response.WriteAsync(result);
        }
    }
}
