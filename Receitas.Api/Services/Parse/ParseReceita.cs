using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseReceita
{
	private ReceitasContext _receitasContext;

	private ParseReceitaIngrediente _parseReceitaIngrediente;
	public ParseReceita(ReceitasContext receitasContext, ParseReceitaIngrediente parseReceitaIngrediente)
	{
		_receitasContext = receitasContext;
		_parseReceitaIngrediente = parseReceitaIngrediente;
	}

	public Receita ParseReceitaDto(ReceitaDTO receitaDTO)
	{
		var ingredientesParsed = _parseReceitaIngrediente.ParseReceitaIngredientesDTOs(receitaDTO.Ingredientes);
		
		var receita = new Receita
		{
			Nome = receitaDTO.Nome,
			Categoria = receitaDTO.Categoria,
			Instrucoes = receitaDTO.Instrucoes,
			Porcoes = receitaDTO.Porcoes,
			Ingredientes = ingredientesParsed
		};
		
		return receita;
}
}
