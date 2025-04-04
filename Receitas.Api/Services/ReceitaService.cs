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

	public ResponseReceitaDTO BuscarPorId(int idReceita)
	{
		ResponseReceitaDTO? receita = _context.Receitas
				.AsNoTracking()
				.Select(_parse.ProjetarEntidadeParaDto())
				.FirstOrDefault(r => r.Id == idReceita);
				
		if(receita == null)
			throw new IdentificadorInvalidoException<Receita>();
			
		return receita;
	}
	
	public List<ResponseReceitaDTO> BuscarTodas()
	{
	    List<ResponseReceitaDTO> receitas = _context.Receitas
			.AsNoTracking()
			.Select(_parse.ProjetarEntidadeParaDto())
			.ToList();
		
		return receitas;
	}

	public ResponseReceitaDTO Inserir(RequestReceitaDTO receitaDTO)
	{
		var receita = _parse.ParseRequestReceitaDto(receitaDTO);
		_context.Add(receita);
		_context.SaveChanges();

		return _parse.ParseResponseReceitaDto(receita);
	}

	public ResponseReceitaDTO Atualizar(RequestReceitaDTO receitaDTO, int idReceita)
	{
		Receita? receita = _context.Receitas.FirstOrDefault(r => r.Id == idReceita);

		if (receita == null)
			throw new IdentificadorInvalidoException<Receita>();

		_parse.ParseRequestReceitaDto(receitaDTO, receita);

		_context.SaveChanges();

		return _parse.ParseResponseReceitaDto(receita);
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
