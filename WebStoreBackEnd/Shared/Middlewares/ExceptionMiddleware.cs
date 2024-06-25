﻿using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Middlewares.Contracts;
using System.Net;

namespace Middlewares.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var errors = ex.Errors
                 .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                 .ToArray();
                var responseError = new ResponseError
                {
                    StatusCode = $"{(int)HttpStatusCode.BadRequest}",
                    Messages = errors
                };
                logger.LogError(ex, responseError.ToString());
                await httpContext.Response.WriteAsync(responseError.ToString());
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var responseError = new ResponseError
                {
                    StatusCode = httpContext.Response.StatusCode.ToString(),
                    Messages = new string[] { ex.Message }
                };
                logger.LogError(ex, responseError.ToString());
                await httpContext.Response.WriteAsync(responseError.ToString());
            }
        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
