using System.Linq.Expressions;
using Receitas.Api.DTO;
using Receitas.Api.Entities;

namespace Receitas.Api.Services.Parse;

public class ParseIngrediente
{

	public Expression<Func<Ingrediente, ResponseIngredienteDTO>> ProjetarEntidadeDto()
	{
	    return i => new ResponseIngredienteDTO()
	    {
	        Id = i.Id,
	        Nome = i.Nome
	    };
	}
	
	public ResponseIngredienteDTO ParseResponseIngredienteDto(Ingrediente i)
	{
		return new ResponseIngredienteDTO()
	    {
	        Id = i.Id,
	        Nome = i.Nome
	    };
	}
	public Ingrediente ParseRequestIngredienteDto(RequestIngredienteDTO ingredienteDTO)
	{
		var ingrediente = new Ingrediente();
		ParseRequestIngredienteDto(ingredienteDTO, ingrediente);
		return ingrediente;
	}
	public Ingrediente ParseRequestIngredienteDto(RequestIngredienteDTO ingredienteDTO, Ingrediente ingrediente)
	{
		ingrediente.Nome = ingredienteDTO.Nome;
		
		return ingrediente;
	}

}
