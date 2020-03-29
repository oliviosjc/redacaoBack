using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redacao.Avaliacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Core.DomainObjects;
using Redacao.Core.Models;
using Redacao.Log.Application.Services.Interface;
using Redacao.Usuario.Application.Services.Interfaces;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : BaseController
    {
		private readonly IAvaliacaoProfessorService _avaliacaoProfessorService;
		private readonly IAvaliacaoRedacaoService _avaliacaoRedacaoService;
		private readonly IUsuarioService _usuarioService;
		private readonly IRedacaoLogService _log;

		public AvaliacaoController(IAvaliacaoProfessorService avaliacaoProfessorService, IAvaliacaoRedacaoService avaliacaoRedacaoService, IUsuarioService usuarioService, IRedacaoLogService log)
		{
			_avaliacaoProfessorService = avaliacaoProfessorService;
			_avaliacaoRedacaoService = avaliacaoRedacaoService;
			_usuarioService = usuarioService;
			_log = log;
		}


		[HttpPost, Route("redacao")]
		[Authorize(Roles="ADMIN")]
		public ActionResult CriarAvaliacaoRedacao(AvaliacaoRedacaoViewModel model)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.Adicionar(model);
				_log.Adicionar("SUCESSO", "Avaliação da Redação feita com sucesso.", "CriarAvaliacaoRedacao", JsonConvert.SerializeObject(model), GetAspNetUserId());
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPut, Route("redacao")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult AtualizarAvaliacaoRedacao(AvaliacaoRedacaoViewModel model)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.Atualizar(model);
				_log.Adicionar("SUCESSO", "Atualização de avaliação da Redação feita com sucesso.", "AtualizarAvaliacaoRedacao", JsonConvert.SerializeObject(model), GetAspNetUserId());
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpGet, Route("redacao/{redacaoId}")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult ObterAvaliacaoRedacao(Guid redacaoId)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.AvaliacaoRedacao(redacaoId);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpGet, Route("aluno/{usuarioId}")]
		public ActionResult AvaliacoesRedacoesUsuarioAluno(Guid usuarioId)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.AvaliacaoesRedacoesUsuarioAluno(usuarioId);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPost, Route("professor")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult CriarAvaliacaoProfessor(AvaliacaoProfessorViewModel model)
		{
			try
			{
				var usuario = _usuarioService.DetalhesUsuario(GetAspNetUserId());
				model.UsuarioAlunoId = usuario.Data.Id;
				var retorno = _avaliacaoProfessorService.Adicionar(model);
				_log.Adicionar("SUCESSO", "Avaliação do professor feita com sucesso.", "CriarAvaliacaoProfessor", JsonConvert.SerializeObject(model), GetAspNetUserId());
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPut, Route("professor")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult AtualizarAvaliacaoProfessor(AvaliacaoProfessorViewModel model)
		{
			try
			{
				var usuario = _usuarioService.DetalhesUsuario(GetAspNetUserId());
				model.UsuarioAlunoId = usuario.Data.Id;
				var retorno = _avaliacaoProfessorService.Atualizar(model);
				_log.Adicionar("SUCESSO", "Atualização da avaliação do professor feita com sucesso.", "AtualizarAvaliacaoProfessor", JsonConvert.SerializeObject(model), GetAspNetUserId());
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpGet, Route("professor/{professorId}")]
		public ActionResult AvaliacoesPorProfessor(Guid professorId)
		{
			try
			{
				var retorno = _avaliacaoProfessorService.AvaliacoesPorProfessor(professorId);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}
	}
}