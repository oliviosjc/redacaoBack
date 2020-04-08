using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Redacao.Avaliacao.Data.Migrations
{
    public partial class InitialAvaliacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvaliacaoProfessor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsuarioProfessorId = table.Column<Guid>(nullable: false),
                    RedacaoId = table.Column<Guid>(nullable: false),
                    QualidadeCorrecao = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoProfessor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoRedacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RedacaoId = table.Column<Guid>(nullable: false),
                    UsuarioAlunoId = table.Column<Guid>(nullable: false),
                    NotaCriterio01 = table.Column<int>(nullable: false),
                    AnotacaoCriterio01 = table.Column<string>(maxLength: 100, nullable: true),
                    NotaCriterio02 = table.Column<int>(nullable: false),
                    AnotacaoCriterio02 = table.Column<string>(maxLength: 100, nullable: true),
                    NotaCriterio03 = table.Column<int>(nullable: false),
                    AnotacaoCriterio03 = table.Column<string>(maxLength: 100, nullable: true),
                    PontosFortes = table.Column<string>(maxLength: 100, nullable: true),
                    PontosFracos = table.Column<string>(maxLength: 100, nullable: true),
                    Feedback = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoRedacao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoProfessor_RedacaoId",
                table: "AvaliacaoProfessor",
                column: "RedacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoProfessor_UsuarioProfessorId",
                table: "AvaliacaoProfessor",
                column: "UsuarioProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoRedacao_RedacaoId",
                table: "AvaliacaoRedacao",
                column: "RedacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoRedacao_UsuarioAlunoId",
                table: "AvaliacaoRedacao",
                column: "UsuarioAlunoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacaoProfessor");

            migrationBuilder.DropTable(
                name: "AvaliacaoRedacao");
        }
    }
}
