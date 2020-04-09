using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Redacao.Core.Data;
using Redacao.Data.Mapping;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedacaoData
{
    public class RedacaoContext : DbContext
    {
        private IConfiguration _configuration;

        public RedacaoContext()
        {

        }

        public RedacaoContext(DbContextOptions<RedacaoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"), options =>
                {
                    options.MigrationsHistoryTable("__RedacoesMigrationsHistory");
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DocumentoMapping());
            modelBuilder.ApplyConfiguration(new RedacaoMapping());
            modelBuilder.ApplyConfiguration(new StatusRedacaoMapping());
            modelBuilder.ApplyConfiguration(new TemaRedacaoMapping());
            modelBuilder.ApplyConfiguration(new TipoRedacaoMapping());
        }

        public bool Commit()
        {
            return  base.SaveChanges() > 0;
        }

        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<Redacao.Domain.Entities.Redacao> Redacao { get; set; }
        public virtual DbSet<StatusRedacao> StatusRedacao { get; set; }
        public virtual DbSet<TemaRedacao> TemaRedacao { get; set; }
        public virtual DbSet<TipoRedacao> TipoRedacao { get; set; }
    }
}
