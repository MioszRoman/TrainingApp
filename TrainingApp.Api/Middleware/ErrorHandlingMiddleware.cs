using System.Net;

namespace TrainingApp.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next =next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try 
        {
            await _next(context);
        }
        catch(Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = "Wystąpił nieoczekiwany błąd serwera."
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
    public class ErrorResponse
    {
        public int StatusCode {get; set;}
        public string Message {get; set;} = "";
    }
}