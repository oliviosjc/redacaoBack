using Redacao.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Repository
{
    public interface IUsuarioRepository : IRepository<Domain.Entities.Usuario>
    {
        void Adicionar(Domain.Entities.Usuario usuario);

        void Atualizar(Domain.Entities.Usuario usuario);

        Domain.Entities.Usuario DetalhesUsuario(Guid usuarioId);

        ICollection<Domain.Entities.Usuario> ListarUsuarios();

        void DesativarUsuario(Guid usuarioId);
    }
}
