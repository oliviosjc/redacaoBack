using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Redacao.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    File = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Extension = table.Column<string>(maxLength: 10, nullable: false),
                    Size = table.Column<string>(nullable: true),
                    Folder = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusRedacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusRedacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemaRedacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemaRedacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoRedacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRedacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Redacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 200, nullable: true),
                    TipoRedacaoId = table.Column<Guid>(nullable: false),
                    TemaRedacaoId = table.Column<Guid>(nullable: false),
                    StatusRedacaoId = table.Column<Guid>(nullable: false),
                    DocumentoId = table.Column<Guid>(nullable: false),
                    UsuarioAlunoId = table.Column<Guid>(nullable: false),
                    UsuarioProfessorId = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Redacao_Documento_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Redacao_StatusRedacao_StatusRedacaoId",
                        column: x => x.StatusRedacaoId,
                        principalTable: "StatusRedacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Redacao_TemaRedacao_TemaRedacaoId",
                        column: x => x.TemaRedacaoId,
                        principalTable: "TemaRedacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Redacao_TipoRedacao_TipoRedacaoId",
                        column: x => x.TipoRedacaoId,
                        principalTable: "TipoRedacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Redacao_DocumentoId",
                table: "Redacao",
                column: "DocumentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Redacao_StatusRedacaoId",
                table: "Redacao",
                column: "StatusRedacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Redacao_TemaRedacaoId",
                table: "Redacao",
                column: "TemaRedacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Redacao_TipoRedacaoId",
                table: "Redacao",
                column: "TipoRedacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Redacao_UsuarioAlunoId",
                table: "Redacao",
                column: "UsuarioAlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Redacao_UsuarioProfessorId",
                table: "Redacao",
                column: "UsuarioProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Redacao");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "StatusRedacao");

            migrationBuilder.DropTable(
                name: "TemaRedacao");

            migrationBuilder.DropTable(
                name: "TipoRedacao");
        }
    }
}
