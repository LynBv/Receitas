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

	public ReceitaIngrediente ParseRequestReceitaIngredienteDTO(RequestReceitaIngredienteDTO receitaIngredienteDTO)
	{
		ReceitaIngrediente receitaIngrediente = new();
		ParseRequestReceitaIngredienteDTO(receitaIngredienteDTO, receitaIngrediente);
		return receitaIngrediente;
	}

	public ReceitaIngrediente ParseRequestReceitaIngredienteDTO
		(RequestReceitaIngredienteDTO receitaIngredienteDTO, ReceitaIngrediente receitaIngrediente)
	{
		var ReceitaIngredienteParsed = _parseIngrediente.ParseRequestIngredienteDto(receitaIngredienteDTO.Ingrediente);

		receitaIngrediente.Ingrediente = ReceitaIngredienteParsed;
		receitaIngrediente.Quantidade = receitaIngredienteDTO.Quantidade;
		receitaIngrediente.UnidadeDeMedida = receitaIngredienteDTO.UnidadeDeMedida;


		return receitaIngrediente;
	}


}
