using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Application.Services
{
    public class DocumentoService : IDocumentoService
    {

        public readonly IDocumentoRepository _documentoRepository;

        public DocumentoService(IDocumentoRepository documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }

        public Documento AdicionarDocumento(DocumentoViewModel documentoVM)
        {
            var documento = new Documento(documentoVM.File, documentoVM.Name, documentoVM.Extension, documentoVM.Size, documentoVM.Folder);
            var novoDocumento = _documentoRepository.Adicionar(documento);
            _documentoRepository.UnitOfWork.Commit();
            return novoDocumento;

        }

        public void AtualizarDocumento(DocumentoViewModel documentoVM)
        {
            var documento = new Documento(documentoVM.File, documentoVM.Name, documentoVM.Extension, documentoVM.Size, documentoVM.Folder);
            documento.AlterarDocumento(documentoVM.Id);
            _documentoRepository.Atualizar(documento);
            _documentoRepository.UnitOfWork.Commit();
        }
    }
}
