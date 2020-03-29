﻿using AutoMapper;
using Redacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Avaliacao.Domain.Entities;
using Redacao.Avaliacao.Domain.Repository;
using Redacao.Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Redacao.Avaliacao.Application.Services
{
	public class AvaliacaoProfessorService : IAvaliacaoProfessorService
	{
		private readonly IAvaliacaoProfessorRepository _repository;
		private readonly IRedacaoService _redacaoService;
		private readonly IMapper _mapper;

		public AvaliacaoProfessorService(IAvaliacaoProfessorRepository repository, IMapper mapper, IRedacaoService redacaoService)
		{
			_repository = repository;
			_mapper = mapper;
			_redacaoService = redacaoService;
		}

		public ReturnRequestViewModel Adicionar(AvaliacaoProfessorViewModel model)
		{
			var retorno = new ReturnRequestViewModel();
			var redacao = _redacaoService.DetalhesRedacao(model.RedacaoId);

			if (redacao.Data.UsuarioAlunoId != model.UsuarioAlunoId)
				throw new Exception("Essa redação não pertence ao usuário logado.");

			var avaliacaoProfessor = new AvaliacaoProfessor(redacao.Data.UsuarioProfessorId, model.RedacaoId, model.QualidadeCorrecao, model.Observacao);


			_repository.Adicionar(avaliacaoProfessor);

			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Message = "A avaliação foi inserida com sucesso.";
			return retorno;
		}

		public ReturnRequestViewModel Atualizar(AvaliacaoProfessorViewModel model)
		{
			var retorno = new ReturnRequestViewModel();
			var avaliacaoProfessor = _repository.ObterPorId(model.RedacaoId);
			if (avaliacaoProfessor == null)
			{
				retorno.HttpCode = HttpStatusCode.BadRequest;
				retorno.Message = "Não foi encontrado nenhuma avaliacao na base de dados.";
			}

			var redacao = _redacaoService.DetalhesRedacao(model.RedacaoId);

			if (redacao.Data.UsuarioAlunoId != model.UsuarioAlunoId)
				throw new Exception("Essa redação não pertence ao usuário logado.");

			avaliacaoProfessor.AlterarQualidadeCorrecao(model.QualidadeCorrecao);
			avaliacaoProfessor.AlterarObservacao(model.Observacao);

			_repository.Atualizar(avaliacaoProfessor);

			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Message = "A avaliação foi atualizada com sucesso.";
			return retorno;
		}

		public ICollection<AvaliacaoProfessorViewModel> AvaliacoesPorProfessor(Guid professorId)
		{
			return _mapper.Map<ICollection<AvaliacaoProfessorViewModel>>(_repository.AvaliacoesPorProfessor(professorId));
		}
	}
}