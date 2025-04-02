using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services.Parse;

namespace Receitas.Api.Services;

public class IngredienteService
{
	private ReceitasContext _context;
	private ParseIngrediente _parse;
	
	public IngredienteService(ReceitasContext context, ParseIngrediente parseIngrediente){
		_context = context;
		_parse = parseIngrediente;
	}
	
	public Ingrediente BuscarPorId(int id)
	{
		Ingrediente? ingrediente = _context.Ingredientes
			.AsNoTracking()
			.FirstOrDefault(i => i.Id == id);
			
		if(ingrediente == null)
			throw new IdentificadorInvalidoException<Ingrediente>();
		
		return ingrediente;
	}
	
	public List<Ingrediente> BuscarTodos()
	{
		List<Ingrediente> ingredientes = _context.Ingredientes
			.AsNoTracking()
			.ToList();
		
		return ingredientes;
	}
	
	public Ingrediente Inserir(RequestIngredienteDTO ingredienteDTO)
	{
		var ingrediente = _parse.ParseRequestIngredienteDto(ingredienteDTO);
		
		_context.Ingredientes.Add(ingrediente);
		_context.SaveChanges();
		
		return ingrediente;
	}
	
	public Ingrediente Atualizar(RequestIngredienteDTO ingredienteDTO, int id)
	{
	
		Ingrediente? ingrediente = _context.Ingredientes.FirstOrDefault(i => i.Id == id);
		
		if(ingrediente == null)
			throw new IdentificadorInvalidoException<Ingrediente>();
		
		var ingredienteNew = _parse.ParseRequestIngredienteDto(ingredienteDTO, ingrediente);
		
		_context.SaveChanges();
		
		return ingredienteNew;
		
	}
	
	public void Excluir(int id)
	{
		Ingrediente? ingrediente = _context.Ingredientes.FirstOrDefault(i => i.Id == id);
		
		if(ingrediente == null)
		{
			throw new IdentificadorInvalidoException<Ingrediente>();
		}
		
		_context.Ingredientes.Remove(ingrediente);
		
		_context.SaveChanges();

	}
}
