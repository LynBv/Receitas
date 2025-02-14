using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record ResponseReceitaIngredienteDTO(
	int Id,
	RequestIngredienteDTO Ingrediente, 
	double Quantidade, 
	EUnidadeDeMedida UnidadeDeMedida);