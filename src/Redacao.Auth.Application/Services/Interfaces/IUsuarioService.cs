using Redacao.Core.Models;
using Redacao.Usuario.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Application.Services.Interfaces
{
	public interface IUsuarioService
	{
		Task<ReturnRequestViewModel> DetalhesUsuario(Guid usuarioId);

		Task<ReturnRequestViewModel> ListarUsuarios();

		Task<ReturnRequestViewModel> AtualizarUsuario(UsuarioViewModel usuario);
	}
}
