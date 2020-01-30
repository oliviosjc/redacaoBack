using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Entities
{
    public class Atividade : Entity
    {
        public Atividade()
        {

        }
        public string Descricao { get; private set; }

        public DateTime Data { get; private set; }

        public Usuario Usuario { get; private set; }

        public Guid UsuarioId { get; set; }
    }   
}
