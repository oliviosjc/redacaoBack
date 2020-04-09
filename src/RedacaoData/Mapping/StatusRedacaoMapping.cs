using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Data.Mapping
{
    public class StatusRedacaoMapping : IEntityTypeConfiguration<StatusRedacao>
    {
        public void Configure(EntityTypeBuilder<StatusRedacao> builder)
        {
            builder.ToTable("StatusRedacao");

            builder.Property(sr => sr.Nome).HasMaxLength(50).IsRequired();
        }
    }
}
