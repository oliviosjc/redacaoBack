using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Redacao.Usuario.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComoConheceu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComoConheceu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    TipoUsuarioId = table.Column<Guid>(nullable: false),
                    ComoConheceuId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_ComoConheceu_ComoConheceuId",
                        column: x => x.ComoConheceuId,
                        principalTable: "ComoConheceu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_TipoUsuario_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atividade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atividade_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_UsuarioId",
                table: "Atividade",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ComoConheceuId",
                table: "Usuario",
                column: "ComoConheceuId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividade");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "ComoConheceu");

            migrationBuilder.DropTable(
                name: "TipoUsuario");
        }
    }
}
