using InternSotatek.Personal.Application.Common.Dtos;
using FluentValidation;
using System.Net;

namespace InternSotatek.Personal.Api.Middlewares;

public class ErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandling> _logger;

    public ErrorHandling(RequestDelegate next, ILogger<ErrorHandling> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex) // FluentValidation
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
            var response = ApiResponse<object>.Error(string.Join("; ", errors), ErrorCode.BadRequest);

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex) // Lỗi khác
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = ApiResponse<object>.Error("Internal server error", ErrorCode.InternalServerError);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
