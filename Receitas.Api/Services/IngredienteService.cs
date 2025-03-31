using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services.Parse;

namespace Receitas.Api.Services;

public class IngredienteService
{
	private ReceitasContext _contex;
	private ParseIngrediente _parse;
	
	public IngredienteService(ReceitasContext context, ParseIngrediente parseIngrediente){
		_contex = context;
		_parse = parseIngrediente;
	}
	
	public Ingrediente Inserir(RequestIngredienteDTO ingredienteDTO)
	{
		var ingrediente = _parse.ParseRequestIngredienteDto(ingredienteDTO);
		
		_contex.Ingredientes.Add(ingrediente);
		_contex.SaveChanges();
		
		return ingrediente;
	}
	
	public Ingrediente Atualizar(RequestIngredienteDTO ingredienteDTO, int id)
	{
	
		Ingrediente? ingrediente = _contex.Ingredientes.FirstOrDefault(i => i.Id == id);
		
		if(ingrediente == null)
		{
			throw new IdentificadorInvalidoException<Ingrediente>();
		}
		
		var ingredienteNew = _parse.ParseRequestIngredienteDto(ingredienteDTO, ingrediente);
		
		_contex.SaveChanges();
		
		return ingredienteNew;
		
	}
	
	public void Excluir(int id)
	{
		Ingrediente? ingrediente = _contex.Ingredientes.FirstOrDefault(i => i.Id == id);
		
		if(ingrediente == null)
		{
			throw new IdentificadorInvalidoException<Ingrediente>();
		}
		
		_contex.Ingredientes.Remove(ingrediente);
		
		_contex.SaveChanges();

	}
}
