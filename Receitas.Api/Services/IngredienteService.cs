using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services.Parse;

namespace Receitas.Api.Services;

public class IngredienteService
{
	private ReceitasContext _context;
	private ParseIngrediente _parse;
	
	public IngredienteService(ReceitasContext context, ParseIngrediente parseIngrediente){
		_context = context;
		_parse = parseIngrediente;
	}
	
	public async Task<ResponseIngredienteDTO> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
	{
		ResponseIngredienteDTO? ingrediente = await _context.Ingredientes
			.AsNoTracking()
			.Select(_parse.ProjetarEntidadeDto())
			.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
			
		if(ingrediente == null)
			throw new IdentificadorInvalidoException<Ingrediente>();
		
		return ingrediente;
	}
	
	public async Task<List<ResponseIngredienteDTO>> BuscarTodosAsync(CancellationToken cancellationToken)
	{
		List<ResponseIngredienteDTO> ingredientes = await _context.Ingredientes
			.AsNoTracking()
			.Select(_parse.ProjetarEntidadeDto())
			.ToListAsync(cancellationToken);
		
		return ingredientes;
	}
	
	public async Task<ResponseIngredienteDTO> InserirAsync(
		RequestIngredienteDTO ingredienteDTO,
		CancellationToken cancellationToken)
	{
		var ingrediente = _parse.ParseRequestIngredienteDto(ingredienteDTO);
		
		await _context.Ingredientes.AddAsync(ingrediente, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		
		return _parse.ParseResponseIngredienteDto(ingrediente);
	}
	
	public async Task<ResponseIngredienteDTO> AtualizarAsync(
		RequestIngredienteDTO ingredienteDTO,
		int id,
		CancellationToken cancellationToken)
	{
	
		Ingrediente? ingrediente = await _context.Ingredientes.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
		
		if(ingrediente == null)
			throw new IdentificadorInvalidoException<Ingrediente>();
		
		var ingredienteNew = _parse.ParseRequestIngredienteDto(ingredienteDTO, ingrediente);
		
		await _context.SaveChangesAsync(cancellationToken);
		
		return _parse.ParseResponseIngredienteDto(ingredienteNew);
		
	}
	
	public async Task ExcluirAsync(int id, CancellationToken cancellationToken)
	{
		Ingrediente? ingrediente = await _context.Ingredientes.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
		
		if(ingrediente == null)
		{
			throw new IdentificadorInvalidoException<Ingrediente>();
		}
		
		_context.Ingredientes.Remove(ingrediente);
		
		await _context.SaveChangesAsync(cancellationToken);

	}
}
