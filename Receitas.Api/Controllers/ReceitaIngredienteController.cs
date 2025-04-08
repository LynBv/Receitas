using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services;

namespace Receitas.Api.Controllers;

[ApiController]
[Route("api/receita/{idReceita}/ingrediente")]
public class ReceitaIngredienteController : ControllerBase
{
	private ReceitaIngredienteService _service;
	public ReceitaIngredienteController(ReceitaIngredienteService service)
	{
		_service = service;
	}

	[HttpGet("{idIngrediente}")]
	public async Task<Results<NotFound, Ok<ResponseReceitaIngredienteDTO>>> GetIngrediente(
		[FromRoute] int idReceita,
		[FromRoute] int idIngrediente,
		CancellationToken cancellationToken)
	{
		try
		{
			var receitaIngrediente = await
			 _service.BuscarPorIdAsync(idReceita, idIngrediente, cancellationToken);
			return TypedResults.Ok(receitaIngrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}

	}

	[HttpGet("")]
	public async Task<Results<NotFound, Ok<List<ResponseReceitaIngredienteDTO>>>> GetTodosIngredientePorReceita(
		[FromRoute] int idReceita,
		CancellationToken cancellationToken)
	{
		try
		{
			var ingredientes = await _service.BuscarTodosPorReceitaAsync(idReceita, cancellationToken);
			return TypedResults.Ok(ingredientes);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}
	
	[HttpPost("")]
	public async Task<Results<BadRequest, Ok<ResponseReceitaIngredienteDTO>>> PostReceitaIngrediente(
		[FromRoute] int idReceita, 
		[FromBody] RequestReceitaIngredienteDTO receitaIngredienteDto,
		CancellationToken cancellationToken)
	{ 
		try
		{
			var receitaIngrediente = await _service.InserirAsync(idReceita, receitaIngredienteDto, cancellationToken);
			return TypedResults.Ok(receitaIngrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}
	
	[HttpPut("{idIngrediente}")]
	public async Task<Results<BadRequest, Ok<ResponseReceitaIngredienteDTO>>> PutReceitaIngriente(
		[FromRoute] int idReceita, 
		[FromRoute] int idIngrediente,
		[FromBody] RequestReceitaIngredienteDTO receitaIngredienteDto,
		CancellationToken cancellationToken)
	{
		try
		{
			var receitaIngrediente = 
				await _service.AtualizarAsync(idReceita, idIngrediente,receitaIngredienteDto, cancellationToken);
			return TypedResults.Ok(receitaIngrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}
	
	[HttpDelete("{idIngrediente}")]
	public async Task<Results<NoContent, NotFound>> DeleteReceitaIngrediente(
		[FromRoute] int idReceita, 
		[FromRoute] int idIngrediente,
		CancellationToken cancellationToken)
	{
	    try
		{
			await _service.ExcluirAsync(idReceita, idIngrediente, cancellationToken);
			return TypedResults.NoContent();
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}

}
