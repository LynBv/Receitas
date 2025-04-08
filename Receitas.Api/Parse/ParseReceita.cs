using System.Linq.Expressions;
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

	public Expression<Func<Receita, ResponseReceitaDTO>> ProjetarEntidadeParaDto()
	{
		return r => new ResponseReceitaDTO()
		{
			Id = r.Id,
			Categoria = r.Categoria,
			Descricao = r.Descricao,
			Dica = r.Dica,
			Instrucoes = r.Instrucoes,
			Nome = r.Nome,
			Porcoes = r.Porcoes,
			TempoDePreparoMinutos = r.TempoDePreparoMinutos,
			ReceitaIngredientes = r.ReceitaIngredientes.Select(ri => new ResponseReceitaIngredienteDTO()
			{
				Id = ri.Id,
				Quantidade = ri.Quantidade,
				ReceitaId = ri.ReceitaId,
				UnidadeDeMedida = ri.UnidadeDeMedida,
				Ingrediente = new ResponseIngredienteDTO()
				{
					Id = ri.Ingrediente!.Id,
					Nome = ri.Ingrediente.Nome
				}
			}).ToList()
        };
	}

	public ResponseReceitaDTO ParseResponseReceitaDto(Receita r)
	{
	    return new ResponseReceitaDTO()
	    {
			Id = r.Id,
			Categoria = r.Categoria,
			Descricao = r.Descricao,
			Dica = r.Dica,
			Instrucoes = r.Instrucoes,
			Nome = r.Nome,
			Porcoes = r.Porcoes,
			TempoDePreparoMinutos = r.TempoDePreparoMinutos,
			ReceitaIngredientes = r.ReceitaIngredientes
			.Select(_parseReceitaIngrediente.ParseReceitaIngredientetoResponseDTO).ToList()
        };
	}
	public Receita ParseRequestReceitaDto(RequestReceitaDTO receitaDTO)
	{
		Receita receita = new();
		ParseRequestReceitaDto(receitaDTO, receita);
		return receita;
	}

	public void ParseRequestReceitaDto(RequestReceitaDTO receitaDTO, Receita receita)
	{
		receita.Nome = receitaDTO.Nome;
		receita.Categoria = receitaDTO.Categoria;
		receita.Instrucoes = receitaDTO.Instrucoes;
		receita.Porcoes = receitaDTO.Porcoes;

		receita.ReceitaIngredientes.RemoveAll(
			ri => !receitaDTO.ReceitaIngredientes.Any(riDTO => riDTO.Id == ri.Id));

		foreach (RequestReceitaIngredienteDTO receitaIngredienteDTO in receitaDTO.ReceitaIngredientes)
		{
			ReceitaIngrediente? receitaIngrediente =
				receita.ReceitaIngredientes.FirstOrDefault(ri => ri.Id == receitaIngredienteDTO.Id);

			if (receitaIngrediente == null)
			{
				receitaIngrediente = _parseReceitaIngrediente.ParseRequestReceitaIngredienteDTO(receitaIngredienteDTO);
				receita.ReceitaIngredientes.Add(receitaIngrediente);
			}
			else
			{
				_parseReceitaIngrediente.ParseRequestReceitaIngredienteDTO(receitaIngredienteDTO, receitaIngrediente);
			}
		}
	}
}
