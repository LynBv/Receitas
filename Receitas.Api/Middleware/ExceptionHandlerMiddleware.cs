using System.Net;
using Receitas.Api.Exceptions;

namespace Receitas.Api.Middleware;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvolkeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception e)
		{
			if (e is IdentificadorInvalidoException)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync(e.Message);
			}

		}

	}
}
