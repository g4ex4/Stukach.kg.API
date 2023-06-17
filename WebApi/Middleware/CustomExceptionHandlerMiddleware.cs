using Domain.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware
{
    public  class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;


            var response = new Response()
            {
                StatusCode = (int)code,
                Message = result,
                IsSuccess = false
            };
            var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            // Отправка JSON-ответа клиенту
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
