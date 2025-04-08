using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;
using Receitas.Api.Exceptions;
using Receitas.Api.Services.Parse;

namespace Receitas.Api.Services;

public class ReceitaIngredienteService
{
    private ReceitasContext _context;
    private ParseReceitaIngrediente _parse;
    private ReceitaService _receitaService;

    public ReceitaIngredienteService(
    ReceitasContext contex,
    ParseReceitaIngrediente parseReceitaIngrediente,
    ReceitaService receitaService)
    {
        _context = contex;
        _parse = parseReceitaIngrediente;
        _receitaService = receitaService;
    }

    public async Task<ResponseReceitaIngredienteDTO> BuscarPorIdAsync(
        int idReceita,
        int idReceitaIngrediente,
        CancellationToken cancellationToken)
    {
        if (! await _receitaService.VerificarSeReceitaExisteAsync(idReceita, cancellationToken))
            throw new IdentificadorInvalidoException<Receita>();

        var ResponseReceitaIngredienteDTO = await _context.ReceitaIngrediente
            .AsNoTracking()
            .Select(_parse.ProjetarEntidadeParaDto())
            .FirstOrDefaultAsync(ri => ri.Id == idReceitaIngrediente, cancellationToken);

        if (ResponseReceitaIngredienteDTO == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        if (ResponseReceitaIngredienteDTO.ReceitaId != idReceita)
            throw new PaiIncompativelException<Receita, ReceitaIngrediente>();

        return ResponseReceitaIngredienteDTO;
    }

    public async Task<List<ResponseReceitaIngredienteDTO>> BuscarTodosPorReceitaAsync(
        int idReceita,
        CancellationToken cancellationToken)
    {
        if (! await _receitaService.VerificarSeReceitaExisteAsync(idReceita, cancellationToken))
            throw new IdentificadorInvalidoException<Receita>();

        List<ResponseReceitaIngredienteDTO> receitaIngredienteDTOs = await _context.ReceitaIngrediente
            .AsNoTracking()
            .Where(ri => ri.ReceitaId == idReceita)
            .Select(_parse.ProjetarEntidadeParaDto())
            .ToListAsync(cancellationToken);
            
        return receitaIngredienteDTOs;
    }

    public async Task<ResponseReceitaIngredienteDTO> InserirAsync(
        int idReceita,
        RequestReceitaIngredienteDTO requestReceitaIngredienteDTO,
        CancellationToken cancellationToken)
    {
        Receita? receita = await _context.Receitas
            .FirstOrDefaultAsync(r => r.Id == idReceita, cancellationToken);

        if (receita == null)
            throw new IdentificadorInvalidoException<Receita>();

        var receitaIngrediente = _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO);
        receita.ReceitaIngredientes.Add(receitaIngrediente);
        await _context.SaveChangesAsync(cancellationToken);

        var receitaIngredienteDTO = _parse.ParseReceitaIngredientetoResponseDTO(receitaIngrediente);

        return receitaIngredienteDTO;
    }

    public async Task<ResponseReceitaIngredienteDTO> AtualizarAsync(
        int idReceita,
        int idReceitaIngrediente,
        RequestReceitaIngredienteDTO requestReceitaIngredienteDTO,
        CancellationToken cancellationToken)
    {
        if (! await _receitaService.VerificarSeReceitaExisteAsync(idReceita, cancellationToken))
            throw new IdentificadorInvalidoException<Receita>();

        ReceitaIngrediente? receitaIngrediente = await _context.ReceitaIngrediente
            .FirstOrDefaultAsync(ri => ri.Id == idReceitaIngrediente, cancellationToken);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        if (receitaIngrediente.ReceitaId != idReceita)
            throw new PaiIncompativelException<Receita, ReceitaIngrediente>();

        _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO, receitaIngrediente);
        await _context.SaveChangesAsync(cancellationToken);

        var receitaIngredienteDTO = _parse.ParseReceitaIngredientetoResponseDTO(receitaIngrediente);

        return receitaIngredienteDTO;
    }

    public async Task ExcluirAsync(int idReceita, int idReceitaIngrediente, CancellationToken cancellationToken)
    {
        if (!await _receitaService.VerificarSeReceitaExisteAsync(idReceita, cancellationToken))
            throw new IdentificadorInvalidoException<Receita>();

        ReceitaIngrediente? receitaIngrediente = 
            await _context.ReceitaIngrediente.FirstOrDefaultAsync(ri => ri.Id == idReceitaIngrediente, cancellationToken);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        if (receitaIngrediente.ReceitaId != idReceita)
            throw new PaiIncompativelException<Receita, ReceitaIngrediente>();

        _context.ReceitaIngrediente.Remove(receitaIngrediente);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
