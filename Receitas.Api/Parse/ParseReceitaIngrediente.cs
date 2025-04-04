using System.Linq.Expressions;
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
	
	//expressao que sera usada para fazer consultas ao banco com entity framework utilizando apenas os campos nescessarios e preenchendo o objeto la mesmo
	public Expression<Func<ReceitaIngrediente, ResponseReceitaIngredienteDTO>> ProjetarEntidadeParaDto()
	{
	    return ri => new ResponseReceitaIngredienteDTO()
		{
			Id = ri.Id,
			Quantidade = ri.Quantidade, 	
			UnidadeDeMedida = ri.UnidadeDeMedida,
			ReceitaId = ri.ReceitaId,
			
			Ingrediente = new ResponseIngredienteDTO()
			{
				Id = ri.Ingrediente!.Id,
				Nome = ri.Ingrediente.Nome
			}
		};
	}
	
	// funcao que pega um objeto inteiro e preenche outro em memoria
	public ResponseReceitaIngredienteDTO ParseReceitaIngredientetoResponseDTO(ReceitaIngrediente ri)
	{
		return new ResponseReceitaIngredienteDTO() 
		{ 
			Id = ri.Id,
			Quantidade = ri.Quantidade,
			UnidadeDeMedida = ri.UnidadeDeMedida,
			ReceitaId = ri.ReceitaId,
			Ingrediente = _parseIngrediente.ParseResponseIngredienteDto(ri.Ingrediente!)
		};
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
