﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Redacao.Usuario.Data;

namespace Redacao.Usuario.Data.Migrations
{
    [DbContext(typeof(UsuarioContext))]
    [Migration("20200130012738_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.Atividade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<string>("Descricao")
                        .HasMaxLength(100);

                    b.Property<Guid>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Atividade");
                });

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.ComoConheceu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ComoConheceu");
                });

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.TipoUsuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TipoUsuario");
                });

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPF");

                    b.Property<Guid>("ComoConheceuId");

                    b.Property<DateTime?>("DataNascimento");

                    b.Property<string>("Email");

                    b.Property<string>("Genero");

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.Property<Guid>("TipoUsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("ComoConheceuId");

                    b.HasIndex("TipoUsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.Atividade", b =>
                {
                    b.HasOne("Redacao.Usuario.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Atividades")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("Redacao.Usuario.Domain.Entities.ComoConheceu", "ComoConheceu")
                        .WithMany("Usuarios")
                        .HasForeignKey("ComoConheceuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Redacao.Usuario.Domain.Entities.TipoUsuario", "TipoUsuario")
                        .WithMany("Usuarios")
                        .HasForeignKey("TipoUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
