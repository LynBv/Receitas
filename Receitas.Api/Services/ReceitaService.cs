using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services.Parse;

namespace Receitas.Api.Services;

public class ReceitaService
{
	private ReceitasContext _context;
	private ParseReceita _parse;

	public ReceitaService(ReceitasContext context, ParseReceita parseReceita)
	{
		_context = context;
		_parse = parseReceita;
	}

	public Receita BuscarPorId(int idReceita)
	{
		Receita? receita = _context.Receitas
				.AsNoTracking()
				.Include(r => r.ReceitaIngredientes)
				.ThenInclude(i => i.Ingrediente)
				.FirstOrDefault(r => r.Id == idReceita);
				
		if(receita == null)
			throw new IdentificadorInvalidoException<Receita>();
			
		return receita;
	}
	
	public List<Receita> BuscarTodas()
	{
	    List<Receita> receitas = _context.Receitas
			.AsNoTracking()
			.Include(r => r.ReceitaIngredientes)
			.ThenInclude(i => i.Ingrediente)
			.ToList();
		
		return receitas;
	}

	public Receita Inserir(RequestReceitaDTO receitaDTO)
	{
		var receita = _parse.ParseRequestReceitaDto(receitaDTO);
		_context.Add(receita);
		_context.SaveChanges();

		return receita;
	}

	public Receita Atualizar(RequestReceitaDTO receitaDTO, int idReceita)
	{
		Receita? receita = _context.Receitas.FirstOrDefault(r => r.Id == idReceita);

		if (receita == null)
			throw new IdentificadorInvalidoException<Receita>();

		_parse.ParseRequestReceitaDto(receitaDTO, receita);

		_context.SaveChanges();

		return receita;
	}

	public void Excluir(int idReceita)
	{
		Receita? receita = _context.Receitas.FirstOrDefault(r => r.Id == idReceita);

		if (receita == null)
			throw new IdentificadorInvalidoException<Receita>();

		_context.Receitas.Remove(receita);
		_context.SaveChanges();

	}
	
	public bool VerificarSeReceitaExiste(int idReceita)
	{
	    bool receitaExiste =_context.Receitas.Any(r => r.Id == idReceita);
        
        return receitaExiste;
	}
}
