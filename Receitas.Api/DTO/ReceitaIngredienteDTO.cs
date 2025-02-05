using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record ReceitaIngredienteDTO(int ID, IngredienteDTO Ingrediente, double Quantidade, EUnidadeDeMedida UnidadeDeMedida);