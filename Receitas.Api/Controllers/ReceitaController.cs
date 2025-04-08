using System.Threading.Tasks;
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
	public async Task<List<ResponseReceitaDTO>> GetReceitas(CancellationToken cancellationToken)
	{
		return await _service.BuscarTodasAsync(cancellationToken);
	}

	[HttpGet("{idReceita}")]
	public async Task<Results<NotFound, Ok<ResponseReceitaDTO>>> GetReceitaPorId(
		[FromRoute] int idReceita,
		CancellationToken cancellationToken)
	{
		try
		{
			var receita = await _service.BuscarPorIdAsync(idReceita, cancellationToken);
			return TypedResults.Ok(receita);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}
	}

	[HttpPost("")]
	public async Task<Results<BadRequest, Ok<ResponseReceitaDTO>>> PostReceita(
		[FromBody] RequestReceitaDTO receitaDTO,
		CancellationToken cancellationToken)
	{
		try
		{
			var receita = await _service.InserirAsync(receitaDTO, cancellationToken);
			return TypedResults.Ok(receita);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();

		}
	}
	
	[HttpPut("{idReceita}")]
	public async Task<Results<BadRequest, Ok<ResponseReceitaDTO>>> PutReceita(
		[FromRoute] int idReceita,
		[FromBody] RequestReceitaDTO receitaDTO,
		CancellationToken cancellationToken)
	{
		try
		{
			var receita = await _service.AtualizarAsync(receitaDTO, idReceita, cancellationToken);
			return TypedResults.Ok(receita);
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.BadRequest();

		}
	}
	

	[HttpDelete("{idReceita}")]
	public async Task<Results<NoContent, NotFound>> DeleteReceita(
		[FromRoute] int idReceita,
		CancellationToken cancellationToken)
	{
		try
		{
			await _service.ExcluirAsync(idReceita, cancellationToken);
			return TypedResults.NoContent();
		}
		catch (IdentificadorInvalidoException)
		{
			return TypedResults.NotFound();
		}

	}

}