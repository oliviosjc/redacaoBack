using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Domain.Entities.Usuario>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.Property(u => u.Nome).HasMaxLength(50).IsRequired();

            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();

            builder.Property(u => u.Telefone).HasMaxLength(11).IsRequired();

            builder.Property(u => u.CPF).HasMaxLength(11);

            builder.HasIndex(u => u.CPF);

            builder.Property(u => u.Genero).HasMaxLength(30);

            builder.HasOne(u => u.TipoUsuario)
                .WithMany(tu => tu.Usuarios)
                .HasForeignKey(f => f.TipoUsuarioId)
                .IsRequired();

            builder.HasOne(u => u.ComoConheceu)
                .WithMany(cc => cc.Usuarios)
                .HasForeignKey(f => f.ComoConheceuId);

            builder.HasMany(u => u.Atividades)
                .WithOne(a => a.Usuario)
                .HasForeignKey(f => f.UsuarioId);

        }
    }
}
