using Receitas.Api.Context;
using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseIngrediente
{
	private ReceitasContext _receitasContext;

	public ParseIngrediente(ReceitasContext receitasContext)
	{
		_receitasContext = receitasContext;
	}

	public Ingrediente ParseIngredienteDto(IngredienteDTO ingredienteDTO)
	{
		var ingrediente = new Ingrediente()
		{
			Nome = ingredienteDTO.Nome
		};
		return ingrediente;
	}

	
}
