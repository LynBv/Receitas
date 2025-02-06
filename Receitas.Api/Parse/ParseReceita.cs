using System.Linq;
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
		ParseReceitaDto(receitaDTO, receita);
		return receita;
	}

	public void ParseReceitaDto(ReceitaDTO receitaDTO, Receita receita)
	{
		receita.Nome = receitaDTO.Nome;
		receita.Categoria = receitaDTO.Categoria;
		receita.Instrucoes = receitaDTO.Instrucoes;
		receita.Porcoes = receitaDTO.Porcoes;

		// O que ja existe em receita e nao existe no DTO, precisa ser apagado.
		receita.Ingredientes.RemoveAll(i => !receitaDTO.Ingredientes.Any(iDTO => iDTO.ID == i.Id)); ;

		// O que existe no DTO mas nao existe na receita, precisa ser adicionado.

		var ingredientesAdicionados = receitaDTO.Ingredientes
			.Where(iDTO => !receita.Ingredientes.Any(i => i.Id == iDTO.ID))
			.Select(dto => _parseReceitaIngrediente.ParseReceitaIngredienteDTO(dto));
			
		receita.Ingredientes.AddRange(ingredientesAdicionados);

		// O que existe tanto no DTO quanto na receita, precisa ser atualizado.
		var ingredientesAtualizados = receita.Ingredientes
			.Where(i => receitaDTO.Ingredientes.Any(iDTO => iDTO.ID == i.Id));

	}
}
