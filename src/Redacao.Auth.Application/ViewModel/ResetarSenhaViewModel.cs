using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.ViewModel
{
	public class ResetarSenhaViewModel
	{
		public string Senha { get; set; }

		public string ConfirmarSenha { get; set; }

		public string Email { get; set; }

		public string Token { get; set; }
	}
}
