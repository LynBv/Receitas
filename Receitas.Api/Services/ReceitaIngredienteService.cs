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

    public ResponseReceitaIngredienteDTO BuscarPorId(int idReceita, int idReceitaIngrediente)
    {
        if (!_receitaService.VerificarSeReceitaExiste(idReceita))
            throw new IdentificadorInvalidoException<Receita>();

        var ResponseReceitaIngredienteDTO = _context.ReceitaIngrediente
            .AsNoTracking()
            .Select(_parse.ProjetarEntidadeParaDto())
            .FirstOrDefault(ri => ri.Id == idReceitaIngrediente);

        if (ResponseReceitaIngredienteDTO == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        if (ResponseReceitaIngredienteDTO.ReceitaId != idReceita)
            throw new PaiIncompativelException<Receita, ReceitaIngrediente>();

        return ResponseReceitaIngredienteDTO;
    }

    public List<ResponseReceitaIngredienteDTO> BuscarTodosPorReceita(int idReceita)
    {
        if (!_receitaService.VerificarSeReceitaExiste(idReceita))
            throw new IdentificadorInvalidoException<Receita>();

        List<ResponseReceitaIngredienteDTO> receitaIngredienteDTOs = _context.ReceitaIngrediente
            .AsNoTracking()
            .Where(ri => ri.ReceitaId == idReceita)
            .Select(_parse.ProjetarEntidadeParaDto())
            .ToList();
            
        return receitaIngredienteDTOs;
    }

    public ResponseReceitaIngredienteDTO Inserir(int idReceita, RequestReceitaIngredienteDTO requestReceitaIngredienteDTO)
    {
        Receita? receita = _context.Receitas
            .FirstOrDefault(r => r.Id == idReceita);

        if (receita == null)
            throw new IdentificadorInvalidoException<Receita>();

        var receitaIngrediente = _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO);
        receita.ReceitaIngredientes.Add(receitaIngrediente);
        _context.SaveChanges();

        var receitaIngredienteDTO = _parse.ParseReceitaIngredientetoResponseDTO(receitaIngrediente);

        return receitaIngredienteDTO;
    }

    public ResponseReceitaIngredienteDTO Atualizar(
        int idReceita,
        int idReceitaIngrediente,
        RequestReceitaIngredienteDTO requestReceitaIngredienteDTO)
    {
        if (!_receitaService.VerificarSeReceitaExiste(idReceita))
            throw new IdentificadorInvalidoException<Receita>();

        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente
            .FirstOrDefault(ri => ri.Id == idReceitaIngrediente);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        if (receitaIngrediente.ReceitaId != idReceita)
            throw new PaiIncompativelException<Receita, ReceitaIngrediente>();

        _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO, receitaIngrediente);
        _context.SaveChanges();

        var receitaIngredienteDTO = _parse.ParseReceitaIngredientetoResponseDTO(receitaIngrediente);

        return receitaIngredienteDTO;
    }

    public void Excluir(int idReceita, int idReceitaIngrediente)
    {
        if (!_receitaService.VerificarSeReceitaExiste(idReceita))
            throw new IdentificadorInvalidoException<Receita>();

        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente.FirstOrDefault(ri => ri.Id == idReceitaIngrediente);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        if (receitaIngrediente.ReceitaId != idReceita)
            throw new PaiIncompativelException<Receita, ReceitaIngrediente>();

        _context.ReceitaIngrediente.Remove(receitaIngrediente);
        _context.SaveChanges();
    }
}
