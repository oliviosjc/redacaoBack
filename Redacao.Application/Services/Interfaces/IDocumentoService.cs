using Redacao.Application.ViewModel;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Application.Services.Interfaces
{
    public interface IDocumentoService
    {
        Documento AdicionarDocumento(DocumentoViewModel documento);

        void AtualizarDocumento(DocumentoViewModel documento);
    }
}
