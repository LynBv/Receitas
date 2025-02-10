using Receitas.Api.Enums;

namespace Receitas.Api.Entities;

public class ReceitaIngrediente
{
	public int Id{ get; set; }
	public int IngredienteId { get; set; } 
	public double Quantidade { get; set; }
	public EUnidadeDeMedida UnidadeDeMedida { get; set; }
	public Ingrediente? Ingrediente { get; set; } 
}
