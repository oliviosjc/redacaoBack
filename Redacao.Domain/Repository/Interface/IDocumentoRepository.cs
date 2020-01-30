using Redacao.Core.Data;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Repository.Interface
{
    public interface IDocumentoRepository : IRepository<Documento>
    {
        Documento Adicionar(Documento documento);

        void Atualizar(Documento documento);
    }
}
