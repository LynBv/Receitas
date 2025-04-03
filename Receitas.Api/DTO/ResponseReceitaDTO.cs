using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record ResponseReceitaDTO{
	public required int Id { get; init; }
	public required string Nome { get; init; }
	public required List<string> Instrucoes { get; init; } 
	public required ECategoria Categoria { get; init; }
	public required List<RequestReceitaIngredienteDTO> ReceitaIngredientes { get; init; }
	public required string Descricao { get; init; }
	public required string? Dica { get; init; }
	public required int? Porcoes { get; init; }
	public required int? TempoDePreparoMinutos { get; init; }
}
