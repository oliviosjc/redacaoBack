using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Entities
{
    public class ComoConheceu : Entity
    {
        public ComoConheceu()
        {

        }
        public string Nome { get; private set; }

        public ICollection<Usuario> Usuarios { get; private set; }
    }
}
