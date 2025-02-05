using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseReceita
{
	private ParseReceitaIngrediente _parseReceitaIngrediente;
	public ParseReceita(ParseReceitaIngrediente parseReceitaIngrediente)
	{
		_parseReceitaIngrediente = parseReceitaIngrediente;
	}

	public Receita ParseReceitaDto(ReceitaDTO receitaDTO)
	{
		Receita receita = new();
		ParseReceitaDto(receitaDTO, ref receita);
		return receita;
	}
	
	public void ParseReceitaDto(ReceitaDTO receitaDTO, ref Receita receita)
	{
		receita.Nome = receitaDTO.Nome;
		receita.Categoria = receitaDTO.Categoria;
		receita.Instrucoes = receitaDTO.Instrucoes;
		receita.Porcoes = receitaDTO.Porcoes;
		
		/* receita.Ingredientes.RemoveAll(x => x.Id == "a") */
		
		// O que ja existe em receita e nao existe no DTO, precisa ser apagado.
		
		
		// O que existe no DTO mas nao existe na receita, precisa ser adicionado.
		
		
		// O que existe tanto no DTO quanto na receita, precisa ser atualizado.
		
		
		
		var ingredientesParsed = receitaDTO.Ingredientes
			.Select(dto => _parseReceitaIngrediente.ParseReceitaIngredienteDTO(dto))
			.ToList();
	}
}
