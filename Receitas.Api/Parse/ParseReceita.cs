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

		receita.ReceitaIngredientes.RemoveAll(
			ri => !receitaDTO.ReceitaIngredientes.Any(riDTO => riDTO.ID == ri.Id));

		foreach (ReceitaIngredienteDTO receitaIngredienteDTO in receitaDTO.ReceitaIngredientes)
		{
			ReceitaIngrediente? receitaIngrediente = 
				receita.ReceitaIngredientes.FirstOrDefault(ri => ri.Id == receitaIngredienteDTO.ID);

			if (receitaIngrediente == null)
			{
				receitaIngrediente = _parseReceitaIngrediente.ParseReceitaIngredienteDTO(receitaIngredienteDTO);
				receita.ReceitaIngredientes.Add(receitaIngrediente);
			}
			else
			{
				_parseReceitaIngrediente.ParseReceitaIngredienteDTO(receitaIngredienteDTO, receitaIngrediente);
			}
		}
	}
}
