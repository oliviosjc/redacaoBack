using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        }
    }
}
