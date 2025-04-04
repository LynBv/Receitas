using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record ResponseReceitaDTO{
	public required int Id { get; init; }
	public required string Nome { get; init; }
	public required List<string> Instrucoes { get; init; } 
	public required ECategoria Categoria { get; init; }
	public required List<ResponseReceitaIngredienteDTO> ReceitaIngredientes { get; init; }
	public  string? Descricao { get; init; }
	public  string? Dica { get; init; }
	public  int? Porcoes { get; init; }
	public int? TempoDePreparoMinutos { get; init; }
}
