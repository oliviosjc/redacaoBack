using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Redacao.Data.Migrations
{
    public partial class fixdocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Folder",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Documento",
                newName: "Nome");

            migrationBuilder.AddColumn<Guid>(
                name: "AmazonS3Id",
                table: "Documento",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Extensao",
                table: "Documento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmazonS3Id",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Extensao",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Documento",
                newName: "Size");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Documento",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Documento",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Folder",
                table: "Documento",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Documento",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
