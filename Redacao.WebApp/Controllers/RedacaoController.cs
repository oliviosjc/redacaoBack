using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Core.DomainObjects;
using Redacao.Usuario.Application.Services.Interfaces;

namespace Redacao.WebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RedacaoController : BaseController
	{

		private readonly IRedacaoService _redacaoService;
		private readonly IUsuarioService _usuarioService;

		public RedacaoController(IRedacaoService redacaoService, IUsuarioService usuarioService)
		{
			_redacaoService = redacaoService;
			_usuarioService = usuarioService;
		}

		[HttpGet, Route("minhas-redacoes")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult ObterMinhasRedacoes()
		{
			try
			{
				var userId = new Guid(this.User.Claims.ToList().FirstOrDefault(f => f.Type == "userId").Value);
				var usuario = _usuarioService.DetalhesUsuario(userId);
				var redacoes = _redacaoService.RedacoesPorUsuario(usuario.Data.Id);
				return RetornoAPI(redacoes);

			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpGet, Route("{id}")]
		[Authorize(Roles = "ALUNO, ADMIN")]
		public ActionResult ObterDetalhesDaRedacao(Guid id)
		{
			try
			{
				var redacao = _redacaoService.DetalhesRedacao(id);
				return RetornoAPI(redacao);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPost]
		[Authorize(Roles = "ALUNO, ADMIN")]
		public ActionResult AdicionarRedacao(RedacaoViewModel model)
		{
			try
			{
				var userId = new Guid(this.User.Claims.ToList().FirstOrDefault(f => f.Type == "userId").Value);
				var usuario = _usuarioService.DetalhesUsuario(userId);
				model.UsuarioAlunoId = usuario.Data.Id;
				var retorno = _redacaoService.AdicionarRedacao(model);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPut, Route("editarRedacao")]
		[Authorize(Roles = "ALUNO, ADMIN")]
		public ActionResult AtualizarRedacao(RedacaoViewModel model)
		{
			try
			{
				var retorno = _redacaoService.AtualizarRedacao(model);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPost, Route("atualizar-redacao-professor/{redacaoId}")]
		[Authorize(Roles = "ALUNO, ADMIN")]
		public ActionResult AtualizarRedacaoProfessor(Guid redacaoId)
		{
			try
			{
				var userId = new Guid(this.User.Claims.ToList().FirstOrDefault(f => f.Type == "userId").Value);
				var usuario = _usuarioService.DetalhesUsuario(userId);
				var retorno = _redacaoService.AtualizarRedacaoProfessor(redacaoId, usuario.Data.Id);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}


		[HttpGet, Route("tipos-redacao")]
		[Authorize(Roles = "ALUNO, ADMIN")]
		public ActionResult ObterTiposRedacao()
        {
            try
            {
                var tipos = _redacaoService.ObterTiposRedacao();
				return RetornoAPI(tipos);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

        [HttpGet, Route("temas-redacao")]
		[Authorize(Roles = "ALUNO, ADMIN")]
		public ActionResult ObterTemasRedacao()
        {
            try
            {
                var temas = _redacaoService.ObterTemasRedacao();
				return RetornoAPI(temas);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}
    }
}