using Microsoft.EntityFrameworkCore;
using Receitas.Api.Entities;

namespace Receitas.Api.Context;

public class ReceitasContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<Receita> Receitas { get; set; }
	public DbSet<Ingrediente> Ingredientes { get; set; }
	public DbSet<ReceitaIngrediente> ReceitaIngrediente { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Receita>()
			.HasMany(r => r.Ingredientes)
			.WithOne()
			.IsRequired();
			
		modelBuilder.Entity<ReceitaIngrediente>()
			.HasOne(ri => ri.Ingrediente)
			.WithMany()
			.IsRequired();
	}
}
