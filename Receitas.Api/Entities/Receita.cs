using Receitas.Api.Enums;

namespace Receitas.Api.Entities
{
	public class Receita
	{
		public int Id { get; set; }
		public string Nome { get; set; } = string.Empty;
		public List<string> Instrucoes { get; set; } = [];
		public ECategoria Categoria { get; set; }
		public string? Descricao { get; set; }
		public string? Dica { get; set; }
		public int? Porcoes { get; set; }
		public int? TempoDePreparoMinutos { get; set; }
		public List<ReceitaIngrediente> ReceitaIngredientes { get; set; } = [];
	}
}