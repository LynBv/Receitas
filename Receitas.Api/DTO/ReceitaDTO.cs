using Receitas.Api.Enums;

namespace Receitas.Api.DTO;

public record ReceitaDTO(int Id, string Nome, List<string> Instrucoes, ECategoria Categoria, List<ReceitaIngredienteDTO> Ingredientes, string Descricao, string? Dica, int? Porcoes, int? TempoDePreparoMinutos);
