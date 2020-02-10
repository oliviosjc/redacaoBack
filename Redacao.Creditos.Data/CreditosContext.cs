using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Redacao.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Creditos.Data
{
    public class CreditosContext : DbContext, IUnitOfWork
    {
        private IConfiguration _configuration;

        public CreditosContext()
        {

        }

        public CreditosContext(DbContextOptions<CreditosContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"), options =>
                {
                    options.MigrationsHistoryTable("__CreditosMigrationsHistory");
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new DocumentoMapping());
            //modelBuilder.ApplyConfiguration(new RedacaoMapping());
            //modelBuilder.ApplyConfiguration(new StatusRedacaoMapping());
            //modelBuilder.ApplyConfiguration(new TemaRedacaoMapping());
            //modelBuilder.ApplyConfiguration(new TipoRedacaoMapping());
        }

        public bool Commit()
        {
            return base.SaveChanges() > 0;
        }

        //public virtual DbSet<Documento> Documento { get; set; }
        //public virtual DbSet<Redacao.Domain.Entities.Redacao> Redacao { get; set; }
        //public virtual DbSet<StatusRedacao> StatusRedacao { get; set; }
        //public virtual DbSet<TemaRedacao> TemaRedacao { get; set; }
        //public virtual DbSet<TipoRedacao> TipoRedacao { get; set; }
    }
}
