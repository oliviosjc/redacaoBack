using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Data.Mapping
{
    public class RedacaoMapping : IEntityTypeConfiguration<Domain.Entities.Redacao>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Redacao> builder)
        {
            builder.ToTable("Redacao");

            builder.Property(r => r.Descricao).HasMaxLength(200);

            builder.HasOne(r => r.TipoRedacao)
                .WithMany(tr => tr.Redacoes)
                .HasForeignKey(f => f.TipoRedacaoId);

            builder.HasOne(r => r.TemaRedacao)
                .WithMany(tr => tr.Redacoes)
                .HasForeignKey(f => f.TemaRedacaoId);

            builder.HasOne(r => r.StatusRedacao)
                .WithMany(sr => sr.Redacoes)
                .HasForeignKey(f => f.StatusRedacaoId);

            builder.HasOne(r => r.Documento)
                .WithOne(d => d.Redacao)
                .HasForeignKey<Domain.Entities.Redacao>(r => r.DocumentoId);

            builder.Property(r => r.UsuarioAlunoId).IsRequired();

            builder.HasIndex(r => r.UsuarioAlunoId);

            builder.Property(r => r.Ativo).IsRequired();
        }
    }
}
