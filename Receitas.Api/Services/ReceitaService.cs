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
	
	public ReceitaService(ReceitasContext context, ParseReceita parseReceita){
		_context = context;
		_parseReceita = parseReceita;
	}
	
	public Receita Inserir(ReceitaDTO receitaDTO)
	{
		var receita = _parseReceita.ParseReceitaDto(receitaDTO);
		_context.Add(receita);
		_context.SaveChanges();
		
		return receita;
	}
	
	public Receita Atualizar( ReceitaDTO receitaDTO, int id)
	{
		Receita? receita = _context.Receitas.FirstOrDefault(r => r.Id == id);
	  
	  	if (receita == null)
		{
			throw new IdentificadorInvalidoException("não foi encontrada receita com este identificador");
		}
	  
	  	_parseReceita.ParseReceitaDto(receitaDTO, receita);
	  
	  	_context.SaveChanges();
	  
	  	return receita;
	}
}
