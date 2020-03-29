using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Redacao.Core.Data;
using Redacao.Usuario.Data.Mapping;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Data
{
    public class UsuarioContext : DbContext
    {
        private IConfiguration _configuration;

        public UsuarioContext()
        {

        }

        public UsuarioContext(DbContextOptions<UsuarioContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AtividadeMapping());
            modelBuilder.ApplyConfiguration(new ComoConheceuMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
			modelBuilder.ApplyConfiguration(new UsuarioCreditoMapping());

        }

		public virtual DbSet<Atividade> Atividade { get; set; }
        public virtual DbSet<ComoConheceu> ComoConheceu { get; set; }
        public virtual DbSet<Domain.Entities.Usuario> Usuario { get; set; }
		public virtual DbSet<UsuarioCredito> UsuarioCredito { get; set; }
    }
}
