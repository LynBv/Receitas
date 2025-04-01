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

	[HttpGet("{idReceita}")]
	public Results<NotFound, Ok<Receita>> GetReceitaPorId([FromRoute] int idReceita)
	{
		try
		{
			var receita = _service.BuscarPorId(idReceita);
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
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();

		}
	}

	[HttpDelete("{idReceita}")]
	public Results<NoContent, NotFound> DeleteReceita([FromRoute] int idReceita)
	{
		try
		{
			_service.Excluir(idReceita);
			return TypedResults.NoContent();
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}

	}

}