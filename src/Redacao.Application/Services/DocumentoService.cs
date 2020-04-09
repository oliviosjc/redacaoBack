using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Core.Models;
using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Application.Services
{
    public class DocumentoService : IDocumentoService
    {

        public readonly IDocumentoRepository _documentoRepository;

        public DocumentoService(IDocumentoRepository documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }

        public async Task<ReturnRequestViewModel> AdicionarDocumentoAWSS3()
        {
			try
			{
				string filename = @"C:\Users\consultor5a.olivio\OneDrive - Webmotors S.A\Documentos\xx.jpg";
				FileInfo file = new FileInfo(filename);

				using (var client = new AmazonS3Client("AKIAJS7MLLGXI5EKVKIQ", "aFZLXjylQ1cdulBarlZXMjigf/5lGPzYwAKLUd4k", RegionEndpoint.USEast2))
				{
					using (var newMemoryStream = new MemoryStream())
					{

						var uploadRequest = new TransferUtilityUploadRequest
						{
							InputStream = file.OpenRead(),
							Key = "testxe.jpg",
							BucketName = "bucketredacaoapp",
							CannedACL = S3CannedACL.PublicRead
						};

						var fileTransferUtility = new TransferUtility(client);
						var result = fileTransferUtility.UploadAsync(uploadRequest);

					}
				}


				var retorno = new ReturnRequestViewModel();
				return retorno;
			}
			catch(Exception ex)
			{
				throw ex;
			}
        }

        public void AtualizarDocumento(DocumentoViewModel documentoVM)
        {
            //var documento = new Documento(documentoVM.File, documentoVM.Name, documentoVM.Extension, documentoVM.Size, documentoVM.Folder);
            //documento.AlterarDocumento(documentoVM.Id);
            //_documentoRepository.Atualizar(documento);
        }
    }
}
