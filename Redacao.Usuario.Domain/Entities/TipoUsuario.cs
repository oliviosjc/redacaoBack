using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Entities
{
    public class TipoUsuario : Entity
    {
        public TipoUsuario()
        {

        }
        public string Nome { get; private set; }

        public ICollection<Usuario> Usuarios { get; private set; }
    }
}
