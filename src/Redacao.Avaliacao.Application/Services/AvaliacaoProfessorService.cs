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
	public class AvaliacaoProfessorService : IAvaliacaoProfessorService
	{
		private readonly IAvaliacaoProfessorRepository _repository;
		private readonly IRedacaoService _redacaoService;
		private readonly IMapper _mapper;
		private readonly IRedacaoLogService _log;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<Usuario.Domain.Entities.Usuario> _userManager;
		private readonly ICoreServices _coreServices;
		private Guid? _usuarioLogadoId = new Guid();

		public AvaliacaoProfessorService(IAvaliacaoProfessorRepository repository,
									     IMapper mapper, 
										 IRedacaoService redacaoService,
										 IRedacaoLogService log,
										 IHttpContextAccessor httpContextAccessor,
										 ICoreServices coreServices)
		{
			_repository = repository;
			_mapper = mapper;
			_redacaoService = redacaoService;
			_log = log;
			_httpContextAccessor = httpContextAccessor;
			_coreServices = coreServices;
			_usuarioLogadoId = _coreServices.GetLoggedUserId(_httpContextAccessor);
		}

		public ReturnRequestViewModel Adicionar(AvaliacaoProfessorViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var redacao = _redacaoService.DetalhesRedacao(model.RedacaoId);

				if (redacao.Data.UsuarioAlunoId != model.UsuarioAlunoId)
				{
					retorno.HttpCode = HttpStatusCode.BadRequest;
					retorno.Message = "Essa redação não pertence ao usuário logado.";
					return retorno;
				}

				var avaliacaoProfessor = new AvaliacaoProfessor(redacao.Data.UsuarioProfessorId, model.RedacaoId, model.QualidadeCorrecao, model.Observacao);
				_repository.Adicionar(avaliacaoProfessor);

				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Message = "A avaliação foi inserida com sucesso.";

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
				retorno.Message = "Ocorreu um erro intero ao adicionar avaliação do professor.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(Adicionar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		public ReturnRequestViewModel Atualizar(AvaliacaoProfessorViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var avaliacaoProfessor = _repository.ObterPorId(model.RedacaoId);
				if (avaliacaoProfessor == null)
				{
					retorno.HttpCode = HttpStatusCode.BadRequest;
					retorno.Message = "Não foi encontrado nenhuma avaliação na base de dados.";
					return retorno;
				}

				var redacao = _redacaoService.DetalhesRedacao(model.RedacaoId);

				if (redacao.Data.UsuarioAlunoId != model.UsuarioAlunoId)
				{
					retorno.HttpCode = HttpStatusCode.BadRequest;
					retorno.Message = "Essa redação não pertence ao usuário logado";
					return retorno;
				}

				if(redacao == null)
				{
					retorno.HttpCode = HttpStatusCode.BadRequest;
					retorno.Message = "A redação não foi encontrada na base de dados.";
					return retorno;
				}

				avaliacaoProfessor.AlterarQualidadeCorrecao(model.QualidadeCorrecao);
				avaliacaoProfessor.AlterarObservacao(model.Observacao);

				_repository.Atualizar(avaliacaoProfessor);

				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Message = "A avaliação foi atualizada com sucesso.";

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
				retorno.Message = "Ocorreu um erro interno ao atualizar a avaliação do professor.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(Atualizar), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		public ReturnRequestViewModel AvaliacoesPorProfessor(Guid professorId)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var avaliacoes = _mapper.Map<ICollection<AvaliacaoProfessorViewModel>>(_repository.AvaliacoesPorProfessor(professorId));

				if (avaliacoes == null)
				{
					retorno.Message = "Não foi encontrada nenhuma avaliação para o professor.";
					retorno.HttpCode = HttpStatusCode.NoContent;
					return retorno;
				}

				retorno.Message = "As avaliações do professor foram encontradas";
				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Data = avaliacoes;
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao buscar as avaliações do professor.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return retorno;
			}
		}
	}
}
