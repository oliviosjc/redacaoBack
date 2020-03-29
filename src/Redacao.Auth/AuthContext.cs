using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Auth.Data
{
	public class AuthContext : IdentityDbContext
	{
		public AuthContext(DbContextOptions<AuthContext> options) : base(options)
		{

		}
	}
}
