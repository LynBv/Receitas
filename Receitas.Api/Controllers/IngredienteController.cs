using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
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
	public List<ResponseIngredienteDTO> GetIngredientes()
	{
		return _service.BuscarTodos();
	}

	[HttpGet("{id}")]
	public Results<NotFound, Ok<ResponseIngredienteDTO>> GetIngrediente([FromRoute] int id)
	{
		try
		{
			var ingrediente = _service.BuscarPorId(id);
			return TypedResults.Ok(ingrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}

	[HttpPost("")]
	public Results<BadRequest, Ok<ResponseIngredienteDTO>> PostIngrediente([FromBody] RequestIngredienteDTO ingredienteDTO)
	{
		try
		{
			var ingrediente = _service.Inserir(ingredienteDTO);
			return TypedResults.Ok(ingrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}

	[HttpPut("{id}")]
	public Results<BadRequest, Ok<ResponseIngredienteDTO>> PutIngrediente(
	[FromRoute] int id, 
	[FromBody] RequestIngredienteDTO ingredienteDTO)
	{
		try
		{
			var ingrediente = _service.Atualizar(ingredienteDTO, id);
			return TypedResults.Ok(ingrediente);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();
		}
	}

	[HttpDelete("{id}")]
	public Results<NoContent, NotFound> DeleteIngrediente([FromRoute] int id)
	{
		try
		{
			_service.Excluir(id);
			return TypedResults.NoContent();
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}
}
