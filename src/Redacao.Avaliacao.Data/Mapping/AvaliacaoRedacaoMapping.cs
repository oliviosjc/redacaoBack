using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Avaliacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Data.Mapping
{
	public class AvaliacaoRedacaoMapping : IEntityTypeConfiguration<AvaliacaoRedacao>
	{
		public void Configure(EntityTypeBuilder<AvaliacaoRedacao> builder)
		{
			builder.ToTable("AvaliacaoRedacao");

			builder.Property(ar => ar.RedacaoId).IsRequired();

			builder.Property(ar => ar.UsuarioAlunoId).IsRequired();

			builder.HasIndex(ar => ar.UsuarioAlunoId);

			builder.HasIndex(ar => ar.RedacaoId);

			builder.Property(ar => ar.NotaCriterio01).IsRequired();

			builder.Property(ar => ar.NotaCriterio02).IsRequired();

			builder.Property(ar => ar.NotaCriterio03).IsRequired();

			builder.Property(ar => ar.AnotacaoCriterio01).HasMaxLength(100);

			builder.Property(ar => ar.AnotacaoCriterio02).HasMaxLength(100);

			builder.Property(ar => ar.AnotacaoCriterio03).HasMaxLength(100);

			builder.Property(ar => ar.PontosFortes).HasMaxLength(100);

			builder.Property(ar => ar.PontosFracos).HasMaxLength(100);

			builder.Property(ar => ar.Feedback).HasMaxLength(100);
		}
	}
}
