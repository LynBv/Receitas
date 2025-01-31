using System.ComponentModel.DataAnnotations;
using Receitas.Api.Enums;

namespace Receitas.Api.Entities;

public class IngredienteNescessario
{
	public int Id{ get; set; }
	
	[Required]
	public Ingrediente? Ingrediente { get; set; } 
	public required double Quantidade { get; set; }
	public required EUnidadeDeMedida UnidadeDeMedida { get; set; }
}