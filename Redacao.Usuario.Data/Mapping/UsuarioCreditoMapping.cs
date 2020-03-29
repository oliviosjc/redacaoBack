using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Data.Mapping
{
	public class UsuarioCreditoMapping : IEntityTypeConfiguration<UsuarioCredito>
	{
		public void Configure(EntityTypeBuilder<UsuarioCredito> builder)
		{
			builder.ToTable("UsuarioCredito");

			builder.HasKey(uc => uc.UsuarioId);

			builder.HasIndex(uc => uc.UsuarioId);

			builder.HasOne(u => u.Usuario)
				.WithOne(uc => uc.UsuarioCredito)
				.HasForeignKey<UsuarioCredito>(uc => uc.UsuarioId);
		}
	}
}
