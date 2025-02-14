using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services.Parse;

namespace Receitas.Api.Services;

public class ReceitaService
{
	private ReceitasContext _context;
	private ParseReceita _parseReceita;

	public ReceitaService(ReceitasContext context, ParseReceita parseReceita)
	{
		_context = context;
		_parseReceita = parseReceita;
	}

	public Receita Inserir(RequestReceitaDTO receitaDTO)
	{
		var receita = _parseReceita.ParseRequestReceitaDto(receitaDTO);
		_context.Add(receita);
		_context.SaveChanges();

		return receita;
	}

	public Receita Atualizar(RequestReceitaDTO receitaDTO, int id)
	{
		Receita? receita = _context.Receitas.FirstOrDefault(r => r.Id == id);

		if (receita == null)
		{
			throw new IdentificadorInvalidoException<Receita>();
		}

		_parseReceita.ParseRequestReceitaDto(receitaDTO, receita);

		_context.SaveChanges();

		return receita;
	}

	public void Excluir(int id)
	{

		Receita? receita = _context.Receitas.FirstOrDefault(r => r.Id == id);
		
		if (receita == null)
		{
			throw new IdentificadorInvalidoException<Receita>();
		}
		
		_context.Receitas.Remove(receita);
		_context.SaveChanges();

	}
}
