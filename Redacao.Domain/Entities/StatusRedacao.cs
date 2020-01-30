using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Entities
{
    public class StatusRedacao : Entity
    {
        public string Nome { get; private set; }

        public ICollection<Redacao> Redacoes { get; private set; }

        public StatusRedacao(string nome)
        {
            Nome = nome;
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }
    }
}
