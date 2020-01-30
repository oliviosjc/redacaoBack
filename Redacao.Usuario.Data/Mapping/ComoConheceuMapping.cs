using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Data.Mapping
{
    public class ComoConheceuMapping : IEntityTypeConfiguration<ComoConheceu>
    {
        public void Configure(EntityTypeBuilder<ComoConheceu> builder)
        {
            builder.ToTable("ComoConheceu");

            builder.Property(cc => cc.Nome).HasMaxLength(50).IsRequired();
        }
    }
}
