using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Redacao.Usuario.Data.Migrations
{
    public partial class Initial : Migration
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
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(maxLength: 11, nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    Genero = table.Column<string>(maxLength: 30, nullable: true),
                    TipoUsuarioId = table.Column<Guid>(nullable: false),
                    ComoConheceuId = table.Column<Guid>(nullable: false),
                    AspNetUserId = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "UsuarioCredito",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(nullable: false),
                    DataExpiracaoPlano = table.Column<DateTime>(nullable: true),
                    QuantidadeRedacoesPlano = table.Column<int>(nullable: false),
                    QuantidadePerguntasPlano = table.Column<int>(nullable: false),
                    QuantidadeRedacoesAvulsas = table.Column<int>(nullable: false),
                    QuantidadePerguntasAvulsas = table.Column<int>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCredito", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_UsuarioCredito_Usuario_UsuarioId",
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
                name: "IX_Usuario_AspNetUserId",
                table: "Usuario",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CPF",
                table: "Usuario",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ComoConheceuId",
                table: "Usuario",
                column: "ComoConheceuId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCredito_UsuarioId",
                table: "UsuarioCredito",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividade");

            migrationBuilder.DropTable(
                name: "UsuarioCredito");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "ComoConheceu");

            migrationBuilder.DropTable(
                name: "TipoUsuario");
        }
    }
}
