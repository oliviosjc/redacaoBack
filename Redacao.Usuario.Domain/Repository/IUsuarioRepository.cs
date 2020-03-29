using Redacao.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Domain.Repository
{
    public interface IUsuarioRepository
    {
		void Registrar(Domain.Entities.Usuario usuario);

        void Atualizar(Domain.Entities.Usuario usuario);

        Domain.Entities.Usuario DetalhesUsuario(Guid aspNetUserId);

		Domain.Entities.Usuario DetalhesUsuarioById(Guid usuarioId);

        ICollection<Domain.Entities.Usuario> ListarUsuarios();

        void DesativarUsuario(Guid usuarioId);

		bool ValidarEmail(string email);
    }
}
