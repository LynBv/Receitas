using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record RequestReceitaIngredienteDTO(
	int Id,
	RequestIngredienteDTO Ingrediente, 
	double Quantidade, 
	EUnidadeDeMedida UnidadeDeMedida);