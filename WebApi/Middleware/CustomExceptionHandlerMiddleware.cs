using Domain.Common;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware
{
    public static class CustomExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response
                            .WriteAsync(new Response(
                                context.Response.StatusCode,
                                contextFeature.Error.Message,
                                false)
                            .ToString());

                    }
                });
            });
        }
    }
}
