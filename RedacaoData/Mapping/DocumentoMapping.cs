using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Data.Mapping
{
    public class DocumentoMapping : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("Documento");

            builder.Property(d => d.Name).IsRequired().HasMaxLength(50);

            builder.Property(d => d.Extension).IsRequired().HasMaxLength(10);

            builder.Property(d => d.File).IsRequired().HasMaxLength(50);

            builder.Property(d => d.Folder).IsRequired().HasMaxLength(50);
        }
    }
}
