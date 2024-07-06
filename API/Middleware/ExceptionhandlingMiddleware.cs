using API.Exceptions;
using API.Exceptions.Accounts;
using FluentValidation;
using Newtonsoft.Json;
using Serilog;

namespace API.Middleware
{
    public class ExceptionhandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionhandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(x => $"{x.PropertyName} : {x.ErrorMessage}");
                var message = string.Join(' ', errorMessages);
                await HandleExceptionAsync(httpContext, message, StatusCodes.Status400BadRequest);
            }
            catch (IdentityException ex)
            {
                var message = string.Join(' ', ex.Errors.Select(x=>x.Description));
                await HandleExceptionAsync(httpContext, message, StatusCodes.Status500InternalServerError);
            }
            catch (UnauthorizedException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, StatusCodes.Status401Unauthorized);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, StatusCodes.Status404NotFound);
            }
            catch (ForbiddenException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                await HandleExceptionAsync(httpContext, "Something went wrong", StatusCodes.Status500InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonConvert.SerializeObject(new { error = message });
            Log.Error(result);
            return context.Response.WriteAsync(result);
        }
    }
}

