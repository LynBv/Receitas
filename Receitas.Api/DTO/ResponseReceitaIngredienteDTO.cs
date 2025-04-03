using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record ResponseReceitaIngredienteDTO
{
    public required int Id { get; init; }
    
    public required int ReceitaId { get; init; }
    public required RequestIngredienteDTO Ingrediente { get; init; }
    public required double Quantidade { get; init; }
    public required EUnidadeDeMedida UnidadeDeMedida { get; init; }
}
