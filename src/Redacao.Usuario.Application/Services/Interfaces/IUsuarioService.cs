using Redacao.Usuario.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        void Adicionar(UsuarioViewModel usuario);

        void Atualizar(UsuarioViewModel usuario);

        UsuarioViewModel DetalhesUsuario(Guid usuarioId);

        ICollection<UsuarioViewModel> ListarUsuarios();

        void DesativarUsuario(Guid usuarioId);
    }
}
