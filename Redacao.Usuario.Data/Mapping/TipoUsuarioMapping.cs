using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Data.Mapping
{
    public class TipoUsuarioMapping : IEntityTypeConfiguration<TipoUsuario>
    {
        public void Configure(EntityTypeBuilder<TipoUsuario> builder)
        {
            builder.ToTable("TipoUsuario");

            builder.Property(tu => tu.Nome).HasMaxLength(50).IsRequired();
        }
    }
}
