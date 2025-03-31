using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services;

namespace Receitas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceitaController : ControllerBase
{
	private ReceitaService _service;
	public ReceitaController(ReceitaService service)
	{
		_service = service;
	}

	[HttpGet("")]
	public List<Receita> GetReceitas()
	{
		return _service.BuscarTodas();
	}

	[HttpGet("{id}")]
	public Results<NotFound, Ok<Receita>> GetReceitaPorId(int id)
	{
		try
		{
			var receita = _service.BuscarPorId(id);
			return TypedResults.Ok(receita);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}

	[HttpPost("")]
	public Results<BadRequest, Ok<Receita>> PostReceita([FromBody] RequestReceitaDTO receitaDTO)
	{
		try
		{
			var receita = _service.Inserir(receitaDTO);
			return TypedResults.Ok(receita);
		}
		catch (System.Exception)
		{
			return TypedResults.BadRequest();

		}
	}

	[HttpDelete("{id}")]
	public Results<NoContent, NotFound> DeleteReceita(int id)
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