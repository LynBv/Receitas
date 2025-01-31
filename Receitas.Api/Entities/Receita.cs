using Receitas.Api.Enums;

namespace Receitas.Api.Entities
{
	public class Receita
	{
		public int Id { get; set; }
		public required string Nome { get; set; }
		public required ICollection<string> Instrucoes { get; set; }
		public required ECategoria Categoria { get; set; }
		public required ICollection<IngredienteNescessario> Ingredientes { get; set; }
		public string? Descricao { get; set; }
		public string? Dica { get; set; }
		public int? Porcoes { get; set; }
		public int? TempoDePreparoMinutos { get; set; }
	}
}