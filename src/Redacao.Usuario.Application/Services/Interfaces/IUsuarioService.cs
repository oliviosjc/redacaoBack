using Redacao.Core.Models;
using Redacao.Usuario.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<ReturnRequestViewModel> Registrar(UsuarioViewModel usuario);
		ReturnRequestViewModel Atualizar(UsuarioViewModel usuario);
		ReturnRequestViewModel DetalhesUsuario(Guid aspNetUserId);
		UsuarioViewModel DetalhesUsuarioById(Guid usuarioId);
		ReturnRequestViewModel ListarUsuarios();
		ReturnRequestViewModel DesativarUsuario(Guid usuarioId);
		DashboardUsuarioViewModel Dashboard(Guid usuarioId);
    }
}
