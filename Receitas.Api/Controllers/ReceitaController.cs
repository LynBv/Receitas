using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.Entities;

namespace Receitas.Api.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class ReceitaController : ControllerBase
{
	private ReceitasContext _receitasContext;
	public ReceitaController(ReceitasContext receitasContext)
	{
		_receitasContext = receitasContext;
	}
	
	[HttpGet("")]
	public List<Receita> GetReceitas()
	{
		List<Receita> receitas = _receitasContext.Receitas
			//.Include("Ingredientes.Ingrediente")
			.AsNoTracking()
			.Include(r => r.ReceitaIngredientes)
			.ThenInclude(i => i.Ingrediente)
			.ToList();
		
		return receitas;
	}
	
	[HttpGet("{id}")]
	public Results<NotFound, Ok<Receita>> GetReceitaPorId(int id)
	{
		Receita? receita = _receitasContext.Receitas
			.AsNoTracking()
			.Include(r => r.ReceitaIngredientes)
			.ThenInclude(i => i.Ingrediente)
			.FirstOrDefault(r => r.Id == id);
		
		if(receita == null){
			return TypedResults.NotFound();
		}
		
		return TypedResults.Ok(receita);
		
	}
	
	[HttpPost("")]
	public Results<BadRequest, Ok<Receita>> PostReceita([FromBody] Receita receita)
	{
		_receitasContext.Add(receita);
		_receitasContext.SaveChanges();
		return TypedResults.Ok(receita);	
	}
	
	[HttpDelete("{id}")]
	public Results<NoContent, NotFound> DeleteReceita(int id){
		
	/* 	int deletedRows = _receitaContext.Receitas
			.Where(x => x.Id == id)
			.ExecuteDelete();
		
		return deletedRows > 0
			? TypedResults.Ok()
			: TypedResults.NoContent(); */
		
		
		Receita? receita =_receitasContext.Receitas.FirstOrDefault(r => r.Id == id);
		
		if(receita == null){
			return TypedResults.NotFound();
		}
		
		_receitasContext.Receitas.Remove(receita);
		_receitasContext.SaveChanges();
		
		return TypedResults.NoContent();
	}
	

}