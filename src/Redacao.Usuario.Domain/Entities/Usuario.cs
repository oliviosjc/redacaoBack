using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Redacao.Usuario.Domain.Entities
{
	public class Usuario : IdentityUser
	{
		public string CPF { get; set; }
	}
}
