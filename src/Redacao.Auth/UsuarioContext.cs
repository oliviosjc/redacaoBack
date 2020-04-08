using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Redacao.Usuario.Data
{
	public class UsuarioContext : IdentityDbContext<Domain.Entities.Usuario>
	{
		public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
		{

		}
	}
}
