using Redacao.Core.Models;
using Redacao.Usuario.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Application.Services.Interfaces
{
	public interface IAuthService
	{
		Task<ReturnRequestViewModel> RegistrarUsuario(RegisterUserViewModel model);

		Task<ReturnRequestViewModel> Entrar(LoginUserViewModel model);

		Task<ReturnRequestViewModel> RecuperarSenha(ForgotPasswordViewModel model);

		Task<ReturnRequestViewModel> DesativarUsuario(Guid usuarioId);

		Task<ReturnRequestViewModel> ResetarSenha(ResetPasswordViewModel model);
	}
}
