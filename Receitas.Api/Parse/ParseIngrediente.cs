using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseIngrediente
{
	public Ingrediente ParseIngredienteDto(IngredienteDTO ingredienteDTO)
	{
		var ingrediente = new Ingrediente();
		ParseIngredienteDto(ingredienteDTO, ingrediente);
		return ingrediente;
	}
	public Ingrediente ParseIngredienteDto(IngredienteDTO ingredienteDTO, Ingrediente ingrediente)
	{
		ingrediente.Nome = ingredienteDTO.Nome;
		
		return ingrediente;
	}

}
