using Redacao.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Domain.Repository
{
    public interface IUsuarioRepository
    {
		void Registrar();

        void Atualizar();

		void DetalhesUsuario();

		void DetalhesUsuarioById();

        void ListarUsuarios();

        void DesativarUsuario(Guid usuarioId);

		bool ValidarEmail(string email);
    }
}
