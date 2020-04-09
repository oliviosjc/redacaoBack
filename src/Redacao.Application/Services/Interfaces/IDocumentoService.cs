using Microsoft.AspNetCore.Http;
using Redacao.Application.ViewModel;
using Redacao.Core.Models;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Application.Services.Interfaces
{
    public interface IDocumentoService
    {
        Task<ReturnRequestViewModel> AdicionarDocumentoAWSS3();

        void AtualizarDocumento(DocumentoViewModel documento);
    }
}
