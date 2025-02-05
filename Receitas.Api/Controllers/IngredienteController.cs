using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.Entities;

namespace Receitas.Api.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class IngredienteController : ControllerBase
{
	
	private ReceitasContext _receitasContext;
	
	public IngredienteController(ReceitasContext receitasContext)
	{
		_receitasContext = receitasContext;
	}
	
	[HttpGet("")]
	public List<Ingrediente> GetIngredientes()
	{
		List<Ingrediente> ingredientes = _receitasContext.Ingredientes
			.AsNoTracking()
			.ToList();
		
		return ingredientes;
	}
	
	[HttpGet("{id}")]
	public Results<NotFound, Ok<Ingrediente>> GetIngrediente(int id)
	{
		Ingrediente? ingrediente = _receitasContext.Ingredientes
			.AsNoTracking()
			.FirstOrDefault(i => i.Id == id);
		
		return ingrediente == null 
			? TypedResults.NotFound()
			: TypedResults.Ok(ingrediente);
	}
	
	[HttpPost("")]
	public Results<BadRequest, Ok<Ingrediente>> PostIngrediente(Ingrediente ingrediente)
	{
		_receitasContext.Add(ingrediente);
		_receitasContext.SaveChanges();
		return TypedResults.Ok(ingrediente);
	}
	
	[HttpDelete("{id}")]
	public Results<NoContent, NotFound> DeleteIngrediente(int id)
	{
		Ingrediente? ingrediente = _receitasContext.Ingredientes.FirstOrDefault(i => i.Id == id);
		
		/* ingrediente == null 
		? return TypedResults.NotFound()
		: _receitasContext.Ingredientes.Remove(ingrediente); */
		
		
		if(ingrediente == null){
			return TypedResults.NotFound();
		}
		
		_receitasContext.Ingredientes.Remove(ingrediente);
		_receitasContext.SaveChanges();
		
		return TypedResults.NoContent();

		
	}
}
