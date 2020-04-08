using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Entities
{
    public class Documento : Entity, IAggregateRoot
    {
		public Guid AmazonS3Id { get; private set; }

        public string Nome { get; private set; }

        public string Extensao { get; private set; }

        public Redacao Redacao { get; private set; }

        public Documento(Guid amazonS3Id, string nome, string extensao)
        {
			Nome = nome;
			AmazonS3Id = amazonS3Id;
			Extensao = extensao;
            Validar();
        }

        public void AlterarDocumento(Guid id)
        {
            Validacoes.ValidarSeNulo(id, "O id do documento esta nulo.");    
            Id = id;
        }

        public void Validar()
        {
            Validacoes.ValidarTamanho(Nome, 100, "O campo folder do documento não pode ser maior do que 50 caracteres");
        }
    }
}
