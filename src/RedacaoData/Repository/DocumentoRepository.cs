using Redacao.Core.Data;
using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using RedacaoData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Data.Repository
{
    public class DocumentoRepository : IDocumentoRepository
    {

        private readonly RedacaoContext _context;

        public DocumentoRepository(RedacaoContext context)
        {
            _context = context;
        }

        public Documento Adicionar(Documento documento)
        {
            var novoDocumento = _context.Documento.Add(documento).Entity;
			_context.SaveChanges();
			_context.Dispose();
            return novoDocumento;
        }

        public void Atualizar(Documento documento)
        {
            _context.Documento.Update(documento);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
