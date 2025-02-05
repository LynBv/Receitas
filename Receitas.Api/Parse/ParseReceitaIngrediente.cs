using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseReceitaIngrediente
{
	private ParseIngrediente _parseIngrediente;
	public ParseReceitaIngrediente( ParseIngrediente parseIngrediente)
	{
		_parseIngrediente = parseIngrediente;
	}

	public ReceitaIngrediente ParseReceitaIngredienteDTO(ReceitaIngredienteDTO receitaIngredienteDTO)
	{
		var ingredienteParsed = _parseIngrediente.ParseIngredienteDto(receitaIngredienteDTO.Ingrediente);

		var receitaIngrediente = new ReceitaIngrediente()
		{
			Ingrediente = ingredienteParsed,
			Quantidade = receitaIngredienteDTO.Quantidade,
			UnidadeDeMedida = receitaIngredienteDTO.UnidadeDeMedida
		};

		return receitaIngrediente;
	}
	
	
}
