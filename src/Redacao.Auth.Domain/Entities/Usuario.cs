using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Entities
{
	public class Usuario : IdentityUser
	{
		public string CPF { get; set; }
	}
}
