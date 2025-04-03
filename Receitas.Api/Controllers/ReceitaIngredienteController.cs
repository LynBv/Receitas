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
	public Results<NotFound, Ok<ResponseReceitaIngredienteDTO>> GetIngrediente(
		[FromRoute] int idReceita,
		[FromRoute] int idIngrediente)
	{
		try
		{
			var receitaIngrediente = _service.BuscarPorId(idReceita,idIngrediente);
			return TypedResults.Ok(receitaIngrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}

	}

	[HttpGet("")]
	public Results<NotFound, Ok<List<ReceitaIngrediente>>> GetTodosIngredientePorReceita([FromRoute] int idReceita)
	{
		try
		{
			var ingredientes = _service.BuscarTodosPorReceita(idReceita);
			return TypedResults.Ok(ingredientes);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}
	
	[HttpPost("")]
	public Results<BadRequest, Ok<ReceitaIngrediente>> PostReceitaIngrediente(
		[FromRoute] int idReceita, 
		[FromBody] RequestReceitaIngredienteDTO receitaIngredienteDto)
	{ 
		try
		{
			var receitaIngrediente = _service.Inserir(idReceita, receitaIngredienteDto);
			return TypedResults.Ok(receitaIngrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}
	
	[HttpPut("{idIngrediente}")]
	public Results<BadRequest, Ok<ReceitaIngrediente>> PutReceitaIngriente(
		[FromRoute] int idReceita, 
		[FromRoute] int idIngrediente,
		[FromBody] RequestReceitaIngredienteDTO receitaIngredienteDto)
	{
		try
		{
			var receitaIngrediente = _service.Atualizar(idReceita, idIngrediente,receitaIngredienteDto);
			return TypedResults.Ok(receitaIngrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}
	
	[HttpDelete("{idIngrediente}")]
	public Results<NoContent, NotFound> DeleteReceitaIngrediente(
	[FromRoute] int idReceita, 
	[FromRoute] int idIngrediente)
	{
	    try
		{
			_service.Excluir(idReceita, idIngrediente);
			return TypedResults.NoContent();
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}
	// Lista todos ingredientes da receita
	// GET - api/receita/{id-receita}/ingrediente

	// Lista ingrediente pelo id
	// GET - api/receita/{id-receita}/ingrediente/{id-receita-ingrediente}

	// Inclui ingrediente na receita
	// POST - api/receita/{id-receita}/ingrediente

	// Edita um ingrediente na receita
	// PUT - api/receita/{id-receita}/ingrediente/{id-receita-ingrediente}

	// Exclui um ingrediente na receita
	// DELETE - api/receita/{id-receita}/ingrediente/{id-receita-ingrediente}


}
