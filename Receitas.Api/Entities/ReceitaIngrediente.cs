using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Receitas.Api.Enums;

namespace Receitas.Api.Entities;

public class ReceitaIngrediente
{
	public int Id{ get; set; }
	public Ingrediente? Ingrediente { get; set; } 
	public required double Quantidade { get; set; }
	public required EUnidadeDeMedida UnidadeDeMedida { get; set; }
}