using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Avaliacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Data.Mapping
{
	public class AvaliacaoProfessorMapping : IEntityTypeConfiguration<AvaliacaoProfessor>
	{
		public void Configure(EntityTypeBuilder<AvaliacaoProfessor> builder)
		{
			builder.ToTable("AvaliacaoProfessor");

			builder.HasIndex(ap => ap.UsuarioProfessorId);

			builder.Property(ap => ap.UsuarioProfessorId).IsRequired();

			builder.HasIndex(ap => ap.RedacaoId);

			builder.Property(ap => ap.RedacaoId).IsRequired();

			builder.Property(ap => ap.QualidadeCorrecao).IsRequired();

			builder.Property(ap => ap.Observacao).HasMaxLength(250);
		}
	}
}
