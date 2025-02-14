using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record ResponseReceitaDTO(
	int Id, 
	string Nome, 
	List<string> Instrucoes, 
	ECategoria Categoria, 
	List<RequestReceitaIngredienteDTO> ReceitaIngredientes, 
	string Descricao, 
	string? Dica, 
	int? Porcoes, 
	int? TempoDePreparoMinutos);
