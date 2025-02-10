using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseReceitaIngrediente
{
	private ParseIngrediente _parseIngrediente;
	public ParseReceitaIngrediente(ParseIngrediente parseIngrediente)
	{
		_parseIngrediente = parseIngrediente;
	}

	public ReceitaIngrediente ParseReceitaIngredienteDTO(ReceitaIngredienteDTO receitaIngredienteDTO)
	{
		ReceitaIngrediente receitaIngrediente = new();
		ParseReceitaIngredienteDTO(receitaIngredienteDTO, receitaIngrediente);
		return receitaIngrediente;
	}

	public ReceitaIngrediente ParseReceitaIngredienteDTO(ReceitaIngredienteDTO receitaIngredienteDTO, ReceitaIngrediente receitaIngrediente)
	{
		var ReceitaIngredienteParsed = _parseIngrediente.ParseIngredienteDto(receitaIngredienteDTO.Ingrediente);

		receitaIngrediente.Ingrediente = ReceitaIngredienteParsed;
		receitaIngrediente.Quantidade = receitaIngredienteDTO.Quantidade;
		receitaIngrediente.UnidadeDeMedida = receitaIngredienteDTO.UnidadeDeMedida;


		return receitaIngrediente;
	}


}
