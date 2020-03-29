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
    [Migration("20200324014938_Remove TipoUsuario from Usuario")]
    partial class RemoveTipoUsuariofromUsuario
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

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AspNetUserId");

                    b.Property<bool>("Ativo");

                    b.Property<string>("CPF")
                        .HasMaxLength(11);

                    b.Property<Guid>("ComoConheceuId");

                    b.Property<DateTime?>("DataNascimento");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Genero")
                        .HasMaxLength(30);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.HasKey("Id");

                    b.HasIndex("AspNetUserId");

                    b.HasIndex("CPF");

                    b.HasIndex("ComoConheceuId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.UsuarioCredito", b =>
                {
                    b.Property<Guid>("UsuarioId");

                    b.Property<DateTime?>("DataExpiracaoPlano");

                    b.Property<int>("QuantidadePerguntasAvulsas");

                    b.Property<int>("QuantidadePerguntasPlano");

                    b.Property<int>("QuantidadeRedacoesAvulsas");

                    b.Property<int>("QuantidadeRedacoesPlano");

                    b.Property<decimal>("Saldo");

                    b.HasKey("UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioCredito");
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
                });

            modelBuilder.Entity("Redacao.Usuario.Domain.Entities.UsuarioCredito", b =>
                {
                    b.HasOne("Redacao.Usuario.Domain.Entities.Usuario", "Usuario")
                        .WithOne("UsuarioCredito")
                        .HasForeignKey("Redacao.Usuario.Domain.Entities.UsuarioCredito", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
