using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.Entities;

namespace Receitas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceitaIngredienteController : ControllerBase
{
	private ReceitasContext _receitasContext;
	public ReceitaIngredienteController(ReceitasContext receitasContext)
	{
		_receitasContext = receitasContext;
	}

	[HttpGet("{id}")]
	public Results<NotFound, Ok<ReceitaIngrediente>> GetReceitaIngrediente(int id)
	{
		ReceitaIngrediente? receitaIngrediente = _receitasContext.ReceitaIngrediente
			.AsNoTracking()
			.Include(ri => ri.Ingrediente)
			.FirstOrDefault(ri => ri.Id == id);
												
		if(receitaIngrediente == null){
			return TypedResults.NotFound();
		}
												
		return TypedResults.Ok(receitaIngrediente);
	}
	
}
