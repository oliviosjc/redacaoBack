using AutoMapper;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Core.Enums;
using Redacao.Core.Models;
using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static Redacao.Domain.Enums.RedacaoEnums;

namespace Redacao.Application.Services
{
    public class RedacaoService : IRedacaoService
    {
        private readonly IRedacaoRepository _redacaoRepository;
        private readonly IDocumentoService _documentoService;
        private readonly IMapper _mapper;

        public RedacaoService(IRedacaoRepository redacaoRepository, IMapper mapper, IDocumentoService documentoService)
        {
            _redacaoRepository = redacaoRepository;
            _mapper = mapper;
            _documentoService = documentoService;
        }

        public ReturnRequestViewModel AdicionarRedacao(RedacaoViewModel redacaoVM)
        {
			var retorno = new ReturnRequestViewModel();

            var documento = _documentoService.AdicionarDocumento(redacaoVM.Documento);
			//validacao documento aqui.

            var redacao = new Domain.Entities.Redacao(redacaoVM.Descricao, redacaoVM.TipoRedacaoId, 
                                                      redacaoVM.TemaRedacaoId, StatusRedacaoEnum.INICIADA,
                                                      documento.Id, redacaoVM.UsuarioAlunoId, true);

			_redacaoRepository.Adicionar(redacao);

			retorno.Message = "A redação foi cadastrada com sucesso. Aguarde a correção dos nossos professores.";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = redacao;
			return retorno;
        }

        public ReturnRequestViewModel AtualizarRedacao(RedacaoViewModel redacaoVM)
        {
			var retorno = new ReturnRequestViewModel();
            _documentoService.AtualizarDocumento(redacaoVM.Documento);
            var redacao = _redacaoRepository.DetalhesRedacao(redacaoVM.Id);

			if(redacao == null)
			{
				retorno.Message = "Não foi encontrada nenhuma redação com id " +redacaoVM.Id;
				retorno.HttpCode = HttpStatusCode.BadRequest;
			}

            if (redacaoVM.Ativo)
                redacao.Ativar();
            redacao.Desativar();

            redacao.AlterarDescricao(redacaoVM.Descricao);
            redacao.AlterarAluno(redacaoVM.UsuarioAlunoId);

            var tipoRedacao = _redacaoRepository.ObterTipoRedacao(redacaoVM.TipoRedacaoId);
            var temaRedacao = _redacaoRepository.ObterTemaRedacao(redacaoVM.TemaRedacaoId);

            redacao.AlterarTipoRedacao(tipoRedacao);
            redacao.AlterarTemaRedacao(temaRedacao);
            _redacaoRepository.Atualizar(redacao);

			retorno.Message = "A redação foi atualizada com sucesso.";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = redacao;
			return retorno;
        }

		public ReturnRequestViewModel AtualizarRedacaoProfessor(Guid redacaoId, Guid professorId)
		{
			var retorno = new ReturnRequestViewModel();
			var redacao = _redacaoRepository.DetalhesRedacao(redacaoId);
			if(redacao == null)
			{
				retorno.Message = "Não foi possível encontrar a redação.";
				retorno.HttpCode = HttpStatusCode.BadRequest;
				return retorno;
			}
			redacao.AlterarStatusRedacao(StatusRedacaoEnum.EM_ANALISE);
			redacao.AlterarProfessor(professorId);
			_redacaoRepository.Atualizar(redacao);

			retorno.Message = "A redacao foi atualizada com sucesso.";
			retorno.HttpCode = HttpStatusCode.OK;
			return retorno;
		}

		public ReturnRequestViewModel DetalhesRedacao(Guid redacaoId)
        {
			var retorno = new ReturnRequestViewModel();
            var redacao =  _mapper.Map<RedacaoViewModel>(_redacaoRepository.DetalhesRedacao(redacaoId));
			if(redacao == null)
			{
				retorno.Message = "Nenhuma redação foi encontrada.";
				retorno.HttpCode = HttpStatusCode.NoContent;
			}

			retorno.Message = "A redação foi encontrada.";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = redacao;
			return retorno;
        }

        public ReturnRequestViewModel ObterTemasRedacao()
        {
			var retorno = new ReturnRequestViewModel();
            var temas =  _mapper.Map<ICollection<TemaRedacaoViewModel>>(_redacaoRepository.ObterTemasRedacao());
			if(temas == null)
			{
				retorno.Message = "Não foi encontrado nenhum tema de redação.";
				retorno.HttpCode = HttpStatusCode.NoContent;
				return retorno;
			}

			retorno.Message = "Foram encontrados temas de redação.";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = temas;
			return retorno;
        }

        public ReturnRequestViewModel ObterTiposRedacao()
		{
			var retorno = new ReturnRequestViewModel();
			var tipos =  _mapper.Map<ICollection<TipoRedacaoViewModel>>(_redacaoRepository.ObterTiposRedacao());
			if(tipos == null)
			{
				retorno.Message = "Não foi encontrado nenhum tipo de redação.";
				retorno.HttpCode = HttpStatusCode.NoContent;
				return retorno;
			}

			retorno.Message = "Foram encontrados tipos de redação.";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = tipos;
			return retorno;
		}

        public ReturnRequestViewModel RedacoesPorUsuario(Guid usuarioId)
        {
			var retorno = new ReturnRequestViewModel();
			var redacoes = _mapper.Map<ICollection<RedacaoViewModel>>(_redacaoRepository.RedacoesPorUsuario(usuarioId));
			if(redacoes == null)
			{
				retorno.Message = "Não foi encontrado nenhuma redação do usuário.";
				retorno.HttpCode = HttpStatusCode.NoContent;
				return retorno;
			}

			retorno.Message = "Foram encontrados redações do usuário.";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = redacoes;
			return retorno;
		}
    }
}
