using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Receitas.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Instrucoes = table.Column<string>(type: "TEXT", nullable: false),
                    Categoria = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Dica = table.Column<string>(type: "TEXT", nullable: true),
                    Porcoes = table.Column<int>(type: "INTEGER", nullable: true),
                    TempoDePreparoMinutos = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredienteNescessarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantidade = table.Column<double>(type: "REAL", nullable: false),
                    UnidadeDeMedida = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceitaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredienteNescessarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredienteNescessarios_Ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredienteNescessarios_Receitas_ReceitaId",
                        column: x => x.ReceitaId,
                        principalTable: "Receitas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredienteNescessarios_IngredienteId",
                table: "IngredienteNescessarios",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredienteNescessarios_ReceitaId",
                table: "IngredienteNescessarios",
                column: "ReceitaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredienteNescessarios");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Receitas");
        }
    }
}
