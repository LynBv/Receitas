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

    public ReceitaIngrediente BuscarPorId(int idReceita, int idReceitaIngrediente)
    {
        bool receitaExiste =_context.Receitas.Any(r => r.Id == idReceita);
        
        if(receitaExiste!)
            throw new IdentificadorInvalidoException<Receita>();

        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente
            .FirstOrDefault(ri => ri.Id == idReceitaIngrediente);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        if (receitaIngrediente.ReceitaId != idReceita)
            throw new PaiIncompativelException<Receita, ReceitaIngrediente>();
        
        return receitaIngrediente;
    }

    public List<ReceitaIngrediente> BuscarTodosPorReceita(int idReceita)
    {
        Receita receita = _receitaService.BuscarPorId(idReceita);

        return receita.ReceitaIngredientes;
    }

    public ReceitaIngrediente Inserir(int idReceita, RequestReceitaIngredienteDTO requestReceitaIngredienteDTO)
    {
        Receita? receita = _context.Receitas
            .FirstOrDefault(r => r.Id == idReceita);

        if (receita == null)
            throw new IdentificadorInvalidoException<Receita>();

        var receitaIngrediente = _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO);
        receita.ReceitaIngredientes.Add(receitaIngrediente);
        _context.SaveChanges();

        return receitaIngrediente;
    }

    public ReceitaIngrediente Atualizar(
        int idReceita,
        int idReceitaIngrediente,
        RequestReceitaIngredienteDTO requestReceitaIngredienteDTO)
    {
        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente
            .FirstOrDefault(ri => ri.Id == idReceitaIngrediente);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO, receitaIngrediente);
        _context.SaveChanges();

        return receitaIngrediente;
    }

    public void Excluir(int idReceita, int idReceitaIngrediente)
    {
        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente.FirstOrDefault(ri => ri.Id == idReceitaIngrediente);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        _context.ReceitaIngrediente.Remove(receitaIngrediente);
        _context.SaveChanges();
    }
}
