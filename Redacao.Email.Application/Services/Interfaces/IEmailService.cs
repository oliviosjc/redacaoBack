using Redacao.Email.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Email.Application.Services.Interfaces
{
	public interface IEmailService
	{
		void EnviarEmailRecuperacaoSenha(SendEmailViewModel model);

		void EnviarEmailBoasVindas();

		void EnviarEmailUsuarioDesativado();

		void EnviarEmailResetarSenha();
	}
}
