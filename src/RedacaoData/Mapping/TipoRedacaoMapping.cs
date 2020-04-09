using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Data.Mapping
{
    public class TipoRedacaoMapping : IEntityTypeConfiguration<TipoRedacao>
    {
        public void Configure(EntityTypeBuilder<TipoRedacao> builder)
        {
            builder.ToTable("TipoRedacao");

            builder.Property(tr => tr.Nome).HasMaxLength(50).IsRequired();
        }
    }
}
