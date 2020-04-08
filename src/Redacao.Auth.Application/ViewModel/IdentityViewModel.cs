using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Redacao.Usuario.Application.ViewModel
{
	public class RegisterUserViewModel
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string Email { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "As senhas não conferem.")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Nome { get; set; }

		public string Telefone { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string CPF { get; set; }

		public DateTime? DataNascimento { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Genero { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public Guid ComoConheceuId { get; set; }
	}

	public class LoginUserViewModel
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string Email { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
		public string Password { get; set; }
	}

	public class ForgotPasswordViewModel
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string Email { get; set; }
	}

	public class ResetPasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public string Email { get; set; }
		public string Token { get; set; }
	}
}
