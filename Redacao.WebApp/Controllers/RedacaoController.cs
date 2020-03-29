using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Core.DomainObjects;
using Redacao.Log.Application.Services.Interface;
using Redacao.Usuario.Application.Services.Interfaces;

namespace Redacao.WebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RedacaoController : BaseController
	{

		private readonly IRedacaoService _redacaoService;
		private readonly IUsuarioService _usuarioService;
		private readonly IRedacaoLogService _log;

		public RedacaoController(IRedacaoService redacaoService, IUsuarioService usuarioService, IRedacaoLogService log)
		{
			_redacaoService = redacaoService;
			_usuarioService = usuarioService;
			_log = log;
		}

		[HttpGet, Route("minhas-redacoes")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult ObterMinhasRedacoes()
		{
			try
			{
				var usuario = _usuarioService.DetalhesUsuario(GetAspNetUserId());
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
				var usuario = _usuarioService.DetalhesUsuario(GetAspNetUserId());
				model.UsuarioAlunoId = usuario.Data.Id;
				var retorno = _redacaoService.AdicionarRedacao(model);
				_log.Adicionar("SUCESSO", "Redação cadastrada com sucesso.", "AdicionarRedacao", JsonConvert.SerializeObject(model), GetAspNetUserId());
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
				_log.Adicionar("SUCESSO", "Redação editada com sucesso com sucesso.", "AdicionarRedacao", JsonConvert.SerializeObject(model), GetAspNetUserId());
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
				var usuario = _usuarioService.DetalhesUsuario(GetAspNetUserId());
				var retorno = _redacaoService.AtualizarRedacaoProfessor(redacaoId, usuario.Data.Id);
				_log.Adicionar("SUCESSO", "Professor vinculado a redação.", "AtualizarRedacaoProfessor", "", GetAspNetUserId());
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