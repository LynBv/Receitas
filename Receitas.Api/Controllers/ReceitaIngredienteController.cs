using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Services;

namespace Receitas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceitaIngredienteController : ControllerBase
{
	private ReceitaIngredienteService _service;
	public ReceitaIngredienteController(ReceitaIngredienteService service)
	{
		_service = service;
	}

	[HttpGet("{id}")]
	public Results<NotFound, Ok<ReceitaIngrediente>> GetReceitaIngrediente(int id)
	{
		var receitaIngrediente = _service.BuscarPorId(id);

		if (receitaIngrediente == null)
			return TypedResults.NotFound();

		return TypedResults.Ok(receitaIngrediente);
	}


}
