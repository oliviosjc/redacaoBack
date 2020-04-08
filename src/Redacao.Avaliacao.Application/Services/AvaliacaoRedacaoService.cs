using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Redacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Avaliacao.Domain.Entities;
using Redacao.Avaliacao.Domain.Repository;
using Redacao.Core.DomainObjects;
using Redacao.Core.Enums;
using Redacao.Core.Models;
using Redacao.Core.Services;
using Redacao.Log.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace Redacao.Avaliacao.Application.Services
{
	public class AvaliacaoRedacaoService : IAvaliacaoRedacaoService
	{
		private readonly IAvaliacaoRedacaoRepository _repository;
		private readonly IRedacaoService _redacaoService;
		private readonly IMapper _mapper;
		private readonly IRedacaoLogService _log;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<Usuario.Domain.Entities.Usuario> _userManager;
		private readonly ICoreServices _coreServices;
		private Guid? _usuarioLogadoId = new Guid();

		public AvaliacaoRedacaoService(IAvaliacaoRedacaoRepository repository,
									   IMapper mapper,
									   IRedacaoService redacaoSerice,
									   IRedacaoLogService log,
									   IHttpContextAccessor httpContextAccessor,
									   ICoreServices coreServices)
		{
			_repository = repository;
			_mapper = mapper;
			_redacaoService = redacaoSerice;
			_log = log;
			_httpContextAccessor = httpContextAccessor;
			_coreServices = coreServices;
			_usuarioLogadoId = _coreServices.GetLoggedUserId(_httpContextAccessor);
		}

		public ReturnRequestViewModel Adicionar(AvaliacaoRedacaoViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var redacao = _redacaoService.DetalhesRedacao(model.RedacaoId);
				if(redacao == null)
				{
					redacao.Message = "Não foi encontrado nenhuma redação.";
					redacao.HttpCode = HttpStatusCode.BadRequest;
					return redacao;
				}
				var avaliacaoRedacao = new AvaliacaoRedacao(model.RedacaoId, redacao.Data.UsuarioAlunoId, model.UsuarioProfessorId ,model.NotaCriterio01, model.AnotacaoCriterio01, model.NotaCriterio02, model.AnotacaoCriterio02, model.NotaCriterio03, model.AnotacaoCriterio03, model.PontosFortes, model.PontosFracos, model.Feedback);

				_repository.Adicionar(avaliacaoRedacao);

				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Message = "A avaliação da redação foi feita com sucesso.";

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(Adicionar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(EntityException ex)
			{
				retorno.Message = ex.Message;
				retorno.HttpCode = HttpStatusCode.BadRequest;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(Adicionar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao salvar a avaliação da redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(Adicionar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		public ReturnRequestViewModel Atualizar(AvaliacaoRedacaoViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var avaliacaoRedacao = _repository.AvaliacaoRedacao(model.RedacaoId);

				if (avaliacaoRedacao == null)
				{
					retorno.HttpCode = HttpStatusCode.BadRequest;
					retorno.Message = "Não foi encontrado nenhuma avaliação na base de dados.";
					return retorno;
				}

				avaliacaoRedacao.AlterarAnotacaoCriterio01(model.AnotacaoCriterio01);
				avaliacaoRedacao.AlterarAnotacaoCriterio02(model.AnotacaoCriterio02);
				avaliacaoRedacao.AlterarAnotacaoCriterio03(model.AnotacaoCriterio03);
				avaliacaoRedacao.AlterarNotaCriterio01(model.NotaCriterio01);
				avaliacaoRedacao.AlterarNotaCriterio02(model.NotaCriterio02);
				avaliacaoRedacao.AlterarNotaCriterio03(model.NotaCriterio03);
				avaliacaoRedacao.AlterarPontosFortes(model.PontosFortes);
				avaliacaoRedacao.AlterarPontosFracos(model.PontosFracos);
				avaliacaoRedacao.AlterarFeedback(model.Feedback);

				_repository.Atualizar(avaliacaoRedacao);

				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Message = "A avaliacao da correcao foi feita com sucesso.";

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(Atualizar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(EntityException ex)
			{
				retorno.Message = ex.Message;
				retorno.HttpCode = HttpStatusCode.BadRequest;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(Atualizar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro intero ao atualizar a avaliação da redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(Atualizar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		public ReturnRequestViewModel AvaliacaoesRedacoesUsuarioAluno(Guid alunoId)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var avaliacoes = _mapper.Map<ICollection<AvaliacaoRedacaoViewModel>>(_repository.AvaliacoesRedacoesAluno(alunoId));
				if (avaliacoes == null)
				{
					retorno.Message = "Não foi encontrada nenhuma avaliação pro usuário.";
					retorno.HttpCode = HttpStatusCode.NoContent;
					return retorno;
				}

				retorno.Message = "Foram encontradas avaliações pro usuário.";
				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Data = avaliacoes;
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao buscar as avaliações de redação do usuário.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return retorno;
			}
		}

		public ReturnRequestViewModel AvaliacaoRedacao(Guid redacaoId)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var avaliacao = _mapper.Map<AvaliacaoRedacaoViewModel>(_repository.AvaliacaoRedacao(redacaoId));
				if (avaliacao == null)
				{
					retorno.Message = "Não foi encontrada nenhuma avaliação";
					retorno.HttpCode = HttpStatusCode.NoContent;
					return retorno;
				}

				retorno.Message = "Foi encontrada a avaliação";
				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Data = avaliacao;
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao buscar a avaliação da redação.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return retorno;
			}
		}
	}
}
