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
	
	public ResponseIngredienteDTO BuscarPorId(int id)
	{
		ResponseIngredienteDTO? ingrediente = _context.Ingredientes
			.AsNoTracking()
			.Select(_parse.ProjetarEntidadeDto())
			.FirstOrDefault(i => i.Id == id);
			
		if(ingrediente == null)
			throw new IdentificadorInvalidoException<Ingrediente>();
		
		return ingrediente;
	}
	
	public List<ResponseIngredienteDTO> BuscarTodos()
	{
		List<ResponseIngredienteDTO> ingredientes = _context.Ingredientes
			.AsNoTracking()
			.Select(_parse.ProjetarEntidadeDto())
			.ToList();
		
		return ingredientes;
	}
	
	public ResponseIngredienteDTO Inserir(RequestIngredienteDTO ingredienteDTO)
	{
		var ingrediente = _parse.ParseRequestIngredienteDto(ingredienteDTO);
		
		_context.Ingredientes.Add(ingrediente);
		_context.SaveChanges();
		
		return _parse.ParseResponseIngredienteDto(ingrediente);
	}
	
	public ResponseIngredienteDTO Atualizar(RequestIngredienteDTO ingredienteDTO, int id)
	{
	
		Ingrediente? ingrediente = _context.Ingredientes.FirstOrDefault(i => i.Id == id);
		
		if(ingrediente == null)
			throw new IdentificadorInvalidoException<Ingrediente>();
		
		var ingredienteNew = _parse.ParseRequestIngredienteDto(ingredienteDTO, ingrediente);
		
		_context.SaveChanges();
		
		return _parse.ParseResponseIngredienteDto(ingredienteNew);
		
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
