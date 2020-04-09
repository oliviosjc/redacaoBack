using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Data.Mapping
{
    public class TemaRedacaoMapping : IEntityTypeConfiguration<TemaRedacao>
    {
        public void Configure(EntityTypeBuilder<TemaRedacao> builder)
        {
            builder.ToTable("TemaRedacao");

            builder.Property(tr => tr.Nome).HasMaxLength(50).IsRequired();
        }
    }
}
