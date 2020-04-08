using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.ViewModel
{
	public class CadastroUsuarioViewModel
	{
		public string Email { get; private set; }

		public string Senha { get; private set; }

		public string ConfirmarSenha { get; private set; }

		public string Nome { get; private set; }

		public string Telefone { get; private set; }

		public string CPF { get; private set; }

		public string Genero { get; private set; }
	}
}
