using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Catalog.Application.Exceptions;
using Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ApplicationException =  Catalog.Domain.Exceptions.ApplicationException;

namespace Catalog.API.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private const string MESSAGE_TEMPLATE =
        "Source : {ExceptionHandlingMiddleware}, URL : {Url},  Error :  {Exception}, Inner Exception, {InnerException}";

    private static ILogger _logger;

    public ExceptionHandlingMiddleware(ILoggerFactory logger) =>
        _logger = logger.CreateLogger(nameof(ExceptionHandlingMiddleware));


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}" +
                      $"{context.Request.QueryString}";

            _logger.LogError(MESSAGE_TEMPLATE, nameof(ExceptionHandlingMiddleware), url,
                e.Message, e.InnerException);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = null;

        if (exception is ValidationException validationException) errors = validationException.ErrorsDictionary;

        return errors;
    }
}