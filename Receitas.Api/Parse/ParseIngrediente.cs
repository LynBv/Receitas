using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseIngrediente
{
	public Ingrediente ParseRequestIngredienteDto(RequestIngredienteDTO ingredienteDTO)
	{
		var ingrediente = new Ingrediente();
		ParseRequestIngredienteDto(ingredienteDTO, ingrediente);
		return ingrediente;
	}
	public Ingrediente ParseRequestIngredienteDto(RequestIngredienteDTO ingredienteDTO, Ingrediente ingrediente)
	{
		ingrediente.Nome = ingredienteDTO.Nome;
		
		return ingrediente;
	}

}
