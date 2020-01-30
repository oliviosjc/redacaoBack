using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Entities
{
    public class Documento : Entity, IAggregateRoot
    {
        public string File { get; private set; }

        public string Name { get; private set; }

        public string Extension { get; private set; }

        public string Size { get; private set; }

        public string Folder { get; private set; }

        public Redacao Redacao { get; private set; }

        public Documento(string file, string name, string extension, string size, string folder)
        {
            File = file;
            Name = name;
            Extension = extension;
            Size = size;
            Folder = folder;

            Validar();
        }

        public void AlterarDocumento(Guid id)
        {
            Validacoes.ValidarSeNulo(id, "O id do documento esta nulo.");    
            Id = id;
        }

        public void Validar()
        {
            Validacoes.ValidarTamanho(File, 50, "O campo file do documento não pode ser maior do que 50 caracteres");
            Validacoes.ValidarSeVazio(File, "O campo file do documento não pode ser vazio");

            Validacoes.ValidarTamanho(Name, 50, "O campo name do documento não pode ser maior do que 50 caracteres");
            Validacoes.ValidarSeVazio(Name, "O campo name do documento não pode ser vazio");

            Validacoes.ValidarTamanho(Extension, 10, "O campo extension do documento não pode ser maior do que 10 caracteres");
            Validacoes.ValidarSeVazio(Extension, "O campo extension do documento não pode ser vazio");

            Validacoes.ValidarTamanho(Folder, 50, "O campo folder do documento não pode ser maior do que 50 caracteres");
            Validacoes.ValidarSeVazio(Folder, "O campo folder do documento não pode ser vazio");
        }
    }
}
