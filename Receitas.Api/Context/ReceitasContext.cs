using Microsoft.EntityFrameworkCore;
using Receitas.Api.Entities;

namespace Receitas.Api.Context;

public class ReceitasContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Receita> Receitas { get; set; }
	public DbSet<Ingrediente> Ingredientes { get; set; }
	public DbSet<IngredienteNescessario> IngredienteNescessarios { get; set; }
}
