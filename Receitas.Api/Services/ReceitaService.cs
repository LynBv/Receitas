using System.Threading.Tasks;
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

	public async Task<ResponseReceitaDTO> BuscarPorIdAsync(int idReceita, CancellationToken cancellationToken)
	{
		ResponseReceitaDTO? receita = await _context.Receitas
				.AsNoTracking()
				.Select(_parse.ProjetarEntidadeParaDto())
				.FirstOrDefaultAsync(r => r.Id == idReceita, cancellationToken);
				
		if(receita == null)
			throw new IdentificadorInvalidoException<Receita>();
			
		return receita;
	}
	
	public async Task<List<ResponseReceitaDTO>> BuscarTodasAsync(CancellationToken cancellationToken)
	{
	    List<ResponseReceitaDTO> receitas = await _context.Receitas
			.AsNoTracking()
			.Select(_parse.ProjetarEntidadeParaDto())
			.ToListAsync(cancellationToken);
		
		return receitas;
	}

	public async Task<ResponseReceitaDTO> InserirAsync(RequestReceitaDTO receitaDTO, CancellationToken cancellationToken)
	{
		var receita = _parse.ParseRequestReceitaDto(receitaDTO);
		await _context.AddAsync(receita, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return _parse.ParseResponseReceitaDto(receita);
	}

	public async Task<ResponseReceitaDTO> AtualizarAsync(
		RequestReceitaDTO receitaDTO,
		int idReceita,
		CancellationToken cancellationToken)
	{
		Receita? receita = await _context.Receitas.FirstOrDefaultAsync(r => r.Id == idReceita, cancellationToken);

		if (receita == null)
			throw new IdentificadorInvalidoException<Receita>();

		_parse.ParseRequestReceitaDto(receitaDTO, receita);

		await _context.SaveChangesAsync(cancellationToken);

		return _parse.ParseResponseReceitaDto(receita);
	}

	public async Task ExcluirAsync(int idReceita, CancellationToken cancellationToken)
	{
		Receita? receita = await _context.Receitas.FirstOrDefaultAsync(r => r.Id == idReceita, cancellationToken);

		if (receita == null)
			throw new IdentificadorInvalidoException<Receita>();

		_context.Receitas.Remove(receita);
		await _context.SaveChangesAsync(cancellationToken);

	}
	
	public async Task<bool> VerificarSeReceitaExisteAsync(int idReceita, CancellationToken cancellationToken)
	{
	    bool receitaExiste = await _context.Receitas.AnyAsync(r => r.Id == idReceita, cancellationToken);
        
        return receitaExiste;
	}
}
