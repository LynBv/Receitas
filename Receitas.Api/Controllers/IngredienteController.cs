using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Receitas.Api.DTO;
using Receitas.Api.Exceptions;
using Receitas.Api.Services;

namespace Receitas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredienteController : ControllerBase
{
	private IngredienteService _service;

	public IngredienteController(IngredienteService service)
	{
		_service = service;
	}

	[HttpGet("")]
	public async Task<List<ResponseIngredienteDTO>> GetIngredientesAsync(CancellationToken cancellationToken)
	{
		return await _service.BuscarTodosAsync(cancellationToken);
	}

	[HttpGet("{id}")]
	public async Task<Results<NotFound, Ok<ResponseIngredienteDTO>>> GetIngrediente(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		try
		{
			var ingrediente = await _service.BuscarPorIdAsync(id, cancellationToken);
			return TypedResults.Ok(ingrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}

	[HttpPost("")]
	public async Task<Results<BadRequest, Ok<ResponseIngredienteDTO>>> PostIngrediente(
		[FromBody] RequestIngredienteDTO ingredienteDTO,
		CancellationToken cancellationToken)
	{
		try
		{
			var ingrediente = await
			 _service.InserirAsync(ingredienteDTO, cancellationToken);
			return TypedResults.Ok(ingrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}

	[HttpPut("{id}")]
	public async Task<Results<BadRequest, Ok<ResponseIngredienteDTO>>> PutIngredienteAsync(
		[FromRoute] int id, 
		[FromBody] RequestIngredienteDTO ingredienteDTO,
		CancellationToken cancellationToken)
	{
		try
		{
			var ingrediente = await _service.AtualizarAsync(ingredienteDTO, id, cancellationToken);
			return TypedResults.Ok(ingrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}

	[HttpDelete("{id}")]
	public async Task<Results<NoContent, NotFound>> DeleteIngrediente(
		[FromRoute] int id,
		CancellationToken cancellationToken)
	{
		try
		{
			await _service.ExcluirAsync(id, cancellationToken);
			return TypedResults.NoContent();
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}
}
