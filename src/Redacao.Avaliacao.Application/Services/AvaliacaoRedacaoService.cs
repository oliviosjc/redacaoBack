using AutoMapper;
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
	public class AvaliacaoRedacaoService : IAvaliacaoRedacaoService
	{
		private readonly IAvaliacaoRedacaoRepository _repository;
		private readonly IRedacaoService _redacaoService;
		private readonly IMapper _mapper;

		public AvaliacaoRedacaoService(IAvaliacaoRedacaoRepository repository, IMapper mapper, IRedacaoService redacaoSerice)
		{
			_repository = repository;
			_mapper = mapper;
			_redacaoService = redacaoSerice;
		}

		public ReturnRequestViewModel Adicionar(AvaliacaoRedacaoViewModel model)
		{
			var retorno = new ReturnRequestViewModel();
			var redacao = _redacaoService.DetalhesRedacao(model.RedacaoId);
			var avaliacaoRedacao = new AvaliacaoRedacao(model.RedacaoId, redacao.Data.UsuarioAlunoId, model.NotaCriterio01, model.AnotacaoCriterio01, model.NotaCriterio02, model.AnotacaoCriterio02, model.NotaCriterio03, model.AnotacaoCriterio03, model.PontosFortes, model.PontosFracos, model.Feedback);
			_repository.Adicionar(avaliacaoRedacao);

			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Message = "A avaliação da redação foi feita com sucesso.";

			return retorno;
		}

		public ReturnRequestViewModel Atualizar(AvaliacaoRedacaoViewModel model)
		{
			var retorno = new ReturnRequestViewModel();
			var avaliacaoRedacao = _repository.AvaliacaoRedacao(model.RedacaoId);

			if(avaliacaoRedacao == null)
			{
				retorno.HttpCode = HttpStatusCode.BadRequest;
				retorno.Message = "Não foi encontrado nenhuma avaliacao na base de dados.";
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
			return retorno;
		}

		public ReturnRequestViewModel AvaliacaoesRedacoesUsuarioAluno(Guid alunoId)
		{
			var retorno = new ReturnRequestViewModel();
			var avaliacoes =  _mapper.Map<ICollection<AvaliacaoRedacaoViewModel>>(_repository.AvaliacoesRedacoesAluno(alunoId));
			if(avaliacoes == null)
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

		public ReturnRequestViewModel AvaliacaoRedacao(Guid redacaoId)
		{
			var retorno = new ReturnRequestViewModel();
			var avaliacao = _mapper.Map<AvaliacaoRedacaoViewModel>(_repository.AvaliacaoRedacao(redacaoId));
			if(avaliacao == null)
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
	}
}
