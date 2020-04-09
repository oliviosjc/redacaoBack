using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Core.DomainObjects;
using Redacao.Core.Enums;
using Redacao.Core.Models;
using Redacao.Core.Services;
using Redacao.Domain.Repository.Interface;
using Redacao.Log.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using static Redacao.Domain.Enums.RedacaoEnums;

namespace Redacao.Application.Services
{
	public class RedacaoService : IRedacaoService
    {
		private readonly UserManager<Usuario.Domain.Entities.Usuario> _userManager;
		private readonly IRedacaoRepository _redacaoRepository;
        private readonly IDocumentoService _documentoService;
        private readonly IMapper _mapper;
		private readonly IRedacaoLogService _log;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ICoreServices _coreServices;
		private Guid? _usuarioLogadoId = new Guid();

		public RedacaoService(IRedacaoRepository redacaoRepository, 
							  IMapper mapper,
							  IDocumentoService documentoService,
							  IRedacaoLogService log,
							  IHttpContextAccessor httpContextAccessor,
							  ICoreServices coreServices)
        {
            _redacaoRepository = redacaoRepository;
            _mapper = mapper;
            _documentoService = documentoService;
			_log = log;
			_httpContextAccessor = httpContextAccessor;
			_coreServices = coreServices;
			_usuarioLogadoId = _coreServices.GetLoggedUserId(_httpContextAccessor);
		}

		public ReturnRequestViewModel AdicionarRedacao(RedacaoViewModel redacaoVM)
        {
			var retorno = new ReturnRequestViewModel();

			try
			{
				var documento = _documentoService.AdicionarDocumentoAWSS3();
				//validacao documento aqui.

				//var redacao = new Domain.Entities.Redacao(redacaoVM.Descricao, redacaoVM.TipoRedacaoId,
				//										  redacaoVM.TemaRedacaoId, StatusRedacaoEnum.INICIADA, redacaoVM.UsuarioAlunoId, true);

				//_redacaoRepository.Adicionar(redacao);

				//retorno.Message = "A redação foi cadastrada com sucesso. Aguarde a correção dos nossos professores.";
				//retorno.HttpCode = HttpStatusCode.OK;
				//retorno.Data = redacao;

				//_log.Adicionar(LogLevelEnum.SUCCESS, nameof(AdicionarRedacao), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(EntityException ex)
			{
				retorno.Message = ex.Message;
				retorno.HttpCode = HttpStatusCode.UnprocessableEntity;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(AdicionarRedacao), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno do sistema ao adicionar a redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(AdicionarRedacao), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
        }

        public ReturnRequestViewModel AtualizarRedacao(RedacaoViewModel redacaoVM)
        {
			var retorno = new ReturnRequestViewModel();

			try
			{
				//_documentoService.AtualizarDocumento(redacaoVM.Documento);
				var redacao = _redacaoRepository.DetalhesRedacao(redacaoVM.Id);

				if (redacao == null)
				{
					retorno.Message = "A redação não foi encontrada na base de dados.";
					retorno.HttpCode = HttpStatusCode.BadRequest;
					return retorno;
				}

				if (redacaoVM.Ativo)
					redacao.Ativar();

				if (!redacaoVM.Ativo)
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

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(AtualizarRedacao), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch (EntityException ex)
			{
				retorno.Message = ex.Message;
				retorno.HttpCode = HttpStatusCode.UnprocessableEntity;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(AtualizarRedacao), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno do sistema ao atualizar a redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(AtualizarRedacao), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
        }

		public ReturnRequestViewModel VincularRedacaoProfessor(Guid redacaoId, Guid professorId)
		{
			var retorno = new ReturnRequestViewModel();
			

			try
			{
				var redacao = _redacaoRepository.DetalhesRedacao(redacaoId);
				if (redacao == null)
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

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(VincularRedacaoProfessor), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(EntityException ex)
			{
				retorno.Message = ex.Message;
				retorno.HttpCode = HttpStatusCode.UnprocessableEntity;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(VincularRedacaoProfessor), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno do sistema ao atualizar avaliação do professor.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(VincularRedacaoProfessor), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		public ReturnRequestViewModel DetalhesRedacao(Guid redacaoId)
        {
			var retorno = new ReturnRequestViewModel();
			try
			{
				var redacao = _mapper.Map<RedacaoViewModel>(_redacaoRepository.DetalhesRedacao(redacaoId));
				if (redacao == null)
				{
					retorno.Message = "Nenhuma redação foi encontrada.";
					retorno.HttpCode = HttpStatusCode.NoContent;
				}

				retorno.Message = "A redação foi encontrada.";
				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Data = redacao;
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao obter detalhes da redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return retorno;
			}
        }

        public ReturnRequestViewModel ObterTemasRedacao()
        {
			var retorno = new ReturnRequestViewModel();

			try
			{
				var temas = _mapper.Map<ICollection<TemaRedacaoViewModel>>(_redacaoRepository.ObterTemasRedacao());
				if (temas == null)
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
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao obter temas da redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return retorno;
			}
        }

        public ReturnRequestViewModel ObterTiposRedacao()
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var tipos = _mapper.Map<ICollection<TipoRedacaoViewModel>>(_redacaoRepository.ObterTiposRedacao());
				if (tipos == null)
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
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao obter tipos de redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return retorno;
			}
		}

        public ReturnRequestViewModel RedacoesPorUsuario(Guid usuarioId)
        {
			var retorno = new ReturnRequestViewModel();
			try
			{
				var redacoes = _mapper.Map<ICollection<RedacaoViewModel>>(_redacaoRepository.RedacoesPorUsuario(usuarioId));
				if (redacoes == null)
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
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao obter redações do usuário.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return retorno;
			}
		}
    }
}
