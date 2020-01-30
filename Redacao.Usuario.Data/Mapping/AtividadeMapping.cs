using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Data.Mapping
{
    public class AtividadeMapping : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("Atividade");

            builder.Property(a => a.Descricao)
                .HasMaxLength(100);

            builder.Property(a => a.Data)
                .IsRequired();

            builder.Property(a => a.UsuarioId)
                .IsRequired();
        }
    }
}
