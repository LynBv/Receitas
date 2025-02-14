using System.Net;
using Receitas.Api.Exceptions;

namespace Receitas.Api.Middlewares;

public class CustomExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public CustomExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvolkeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (IdentificadorInvalidoException e)
		{
			context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			await context.Response.WriteAsync(e.Message);
		}
	}
}
