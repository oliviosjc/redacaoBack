using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Entities
{
    public class TemaRedacao : Entity
    {
        public string Nome { get; private set; }

        public ICollection<Redacao> Redacoes { get; private set; }
    }
}
