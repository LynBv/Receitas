using System.Net;
using Receitas.Api.Exceptions;

namespace Receitas.Api.Middlewares;

public class CustomExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
		{
			await next(context);
		}
		catch (IdentificadorInvalidoException e)
		{
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			await context.Response.WriteAsync(e.Message);
		}
    }
}
