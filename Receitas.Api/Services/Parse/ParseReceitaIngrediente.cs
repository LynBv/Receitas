using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseReceitaIngrediente
{
	private ReceitasContext _receitasContext;
	private ParseIngrediente _parseIngrediente;

	public ParseReceitaIngrediente(ReceitasContext receitasContext, ParseIngrediente parseIngrediente)
	{
		_receitasContext = receitasContext;
		_parseIngrediente = parseIngrediente;
	}

	public ReceitaIngrediente parseReceitaIngredienteDTO(ReceitaIngredienteDTO receitaIngredienteDTO)
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

	public List<ReceitaIngrediente> ParseReceitaIngredientesDTOs(List<ReceitaIngredienteDTO> ingredientesDto)
	{
		var ingredientes = ingredientesDto
			.Select(dto => new ReceitaIngrediente 
			{ 
				Ingrediente = _parseIngrediente.ParseIngredienteDto(dto.Ingrediente),
				Quantidade = dto.Quantidade,
				UnidadeDeMedida = dto.UnidadeDeMedida 
			})
			.ToList();

		return ingredientes;
	}
}
