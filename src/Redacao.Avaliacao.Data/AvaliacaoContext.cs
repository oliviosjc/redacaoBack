using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Redacao.Avaliacao.Data.Mapping;
using Redacao.Avaliacao.Domain.Entities;
using Redacao.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Data
{
	public class AvaliacaoContext : DbContext
	{
		private IConfiguration _configuration;

		public AvaliacaoContext()
		{

		}

		public AvaliacaoContext(DbContextOptions<AvaliacaoContext> options, IConfiguration configuration) : base(options)
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
			modelBuilder.ApplyConfiguration(new AvaliacaoProfessorMapping());
			modelBuilder.ApplyConfiguration(new AvaliacaoRedacaoMapping());
		}

		public bool Commit()
		{
			return base.SaveChanges() > 0;
		}

		public virtual DbSet<AvaliacaoProfessor> AvaliacaoProfessor { get; set; }

		public virtual DbSet<AvaliacaoRedacao> AvaliacaoRedacao { get; set; }
	}
}
