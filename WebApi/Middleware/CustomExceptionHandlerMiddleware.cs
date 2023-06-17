using Domain.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware
{
    public  class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomException ex)
        {
            // Обработка вашего пользовательского исключения
            await HandleCustomExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            // Обработка других типов исключений
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleCustomExceptionAsync(HttpContext context, CustomException exception)
    {
        // Формирование ответа с информацией об исключении
        var response = new Response
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = exception.Message,
            IsSuccess = false
        };

        // Преобразование объекта Response в JSON
        var jsonResponse = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        // Отправка JSON-ответа клиенту
        await context.Response.WriteAsync(jsonResponse);
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Формирование ответа с информацией об исключении
        var response = new Response
        {
            StatusCode = 200,
            Message = "Произошла ошибка на сервере.",
            IsSuccess = false
        };

        // Преобразование объекта Response в JSON
        var jsonResponse = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        // Отправка JSON-ответа клиенту
        await context.Response.WriteAsync(jsonResponse);
    }
    }
}
