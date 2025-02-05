using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Services.Parse;

namespace Receitas.Api.Services;

public class IngredienteService
{
	private ReceitasContext _contex;
	private ParseIngrediente _parseIngrediente;
	
	public IngredienteService(ReceitasContext context, ParseIngrediente parseIngrediente){
		_contex = context;
		_parseIngrediente = parseIngrediente;
	}
	
	public Ingrediente Inserir(IngredienteDTO ingredienteDTO)
	{
		var ingrediente = _parseIngrediente.ParseIngredienteDto(ingredienteDTO);
		
		_contex.Ingredientes.Add(ingrediente);
		_contex.SaveChanges();
		
		return ingrediente;
	}
	
	/* public Ingrediente Atualizar(IngredienteDTO ingredienteDTO, int id){

		var ingrediente = _contex.Ingredientes.FirstOrDefault(i => i.Id == id);
		
		
	} */
}
