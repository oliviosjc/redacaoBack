using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redacao.Avaliacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Core.DomainObjects;
using Redacao.Core.Models;
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

		public AvaliacaoController(IAvaliacaoProfessorService avaliacaoProfessorService, IAvaliacaoRedacaoService avaliacaoRedacaoService, IUsuarioService usuarioService)
		{
			_avaliacaoProfessorService = avaliacaoProfessorService;
			_avaliacaoRedacaoService = avaliacaoRedacaoService;
			_usuarioService = usuarioService;
		}


		[HttpPost, Route("redacao")]
		[Authorize(Roles="ADMIN")]
		public ActionResult CriarAvaliacaoRedacao(AvaliacaoRedacaoViewModel model)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.Adicionar(model);
				return StatusCode((int)retorno.HttpCode, retorno.Message);
			}
			catch (EntityException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut, Route("redacao")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult AtualizarAvaliacaoRedacao(AvaliacaoRedacaoViewModel model)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.Atualizar(model);
				return StatusCode((int)retorno.HttpCode, retorno.Message);
			}
			catch (EntityException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet, Route("redacao/{redacaoId}")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult AvaliacaoRedacao(Guid redacaoId)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.AvaliacaoRedacao(redacaoId);
				return Ok(retorno);
			}
			catch (EntityException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet, Route("aluno/{usuarioId}")]
		public ActionResult AvaliacoesRedacoesUsuarioAluno(Guid usuarioId)
		{
			try
			{
				var retorno = _avaliacaoRedacaoService.AvaliacaoesRedacoesUsuarioAluno(usuarioId);
				return Ok(retorno);
			}
			catch (EntityException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost, Route("professor")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult CriarAvaliacaoProfessor(AvaliacaoProfessorViewModel model)
		{
			try
			{
				var userId = new Guid(this.User.Claims.ToList().FirstOrDefault(f => f.Type == "userId").Value);
				var usuario = _usuarioService.DetalhesUsuario(userId);
				model.UsuarioAlunoId = usuario.Data.Id;

				var retorno = _avaliacaoProfessorService.Adicionar(model);
				return StatusCode((int)retorno.HttpCode, retorno.Message);
			}
			catch (EntityException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut, Route("professor")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult AtualizarAvaliacaoProfessor(AvaliacaoProfessorViewModel model)
		{
			try
			{
				var userId = new Guid(this.User.Claims.ToList().FirstOrDefault(f => f.Type == "userId").Value);
				var usuario = _usuarioService.DetalhesUsuario(userId);
				model.UsuarioAlunoId = usuario.Data.Id;
				var retorno = _avaliacaoProfessorService.Atualizar(model);
				return StatusCode((int)retorno.HttpCode, retorno.Message);
			}
			catch (EntityException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet, Route("professor/{professorId}")]
		public ActionResult AvaliacoesPorProfessor(Guid professorId)
		{
			try
			{
				var retorno = _avaliacaoProfessorService.AvaliacoesPorProfessor(professorId);
				return Ok(retorno);
			}
			catch (EntityException ex)
			{
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}