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

    public ReceitaIngredienteService(ReceitasContext contex, ParseReceitaIngrediente parseReceitaIngrediente)
    {
        _context = contex;
        _parse = parseReceitaIngrediente;
    }
    
    public ReceitaIngrediente? BuscarPorId(int id)
    {
        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente
            .AsNoTracking()
			.Include(ri => ri.Ingrediente)
			.FirstOrDefault(ri => ri.Id == id);
        
        return receitaIngrediente;   
    }

    public ReceitaIngrediente Inserir(RequestReceitaIngredienteDTO requestReceitaIngredienteDTO, int receitaId)
    {
        Receita? receita = _context.Receitas.FirstOrDefault(r => r.Id == receitaId);

        if (receita == null)
            throw new IdentificadorInvalidoException<Receita>();


        var receitaIngrediente = _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO);
        receita.ReceitaIngredientes.Add(receitaIngrediente);
        _context.SaveChanges();

        return receitaIngrediente;
    }

    public ReceitaIngrediente Atualizar(RequestReceitaIngredienteDTO requestReceitaIngredienteDTO, int id)
    {
        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente.FirstOrDefault(ri => ri.Id == id);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();

        _parse.ParseRequestReceitaIngredienteDTO(requestReceitaIngredienteDTO, receitaIngrediente);
        _context.SaveChanges();

        return receitaIngrediente;
    }

    public void Excluir(int id)
    {
        ReceitaIngrediente? receitaIngrediente = _context.ReceitaIngrediente.FirstOrDefault(ri => ri.Id == id);

        if (receitaIngrediente == null)
            throw new IdentificadorInvalidoException<ReceitaIngrediente>();
        
        _context.ReceitaIngrediente.Remove(receitaIngrediente);
        _context.SaveChanges();

    }
}
