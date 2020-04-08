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

namespace Redacao.WebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RedacaoController : BaseController
	{

		private readonly IRedacaoService _redacaoService;

		public RedacaoController(IRedacaoService redacaoService)
		{
			_redacaoService = redacaoService;
		}

		[HttpGet, Route("minhas-redacoes")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult ObterMinhasRedacoes()
		{
			try
			{
				var redacoes = _redacaoService.RedacoesPorUsuario(GetAspNetUserId());
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
				model.UsuarioAlunoId = GetAspNetUserId();
				var retorno = _redacaoService.AdicionarRedacao(model);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPut]
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

		[HttpPut, Route("atualizar-professor/{redacaoId}")]
		[Authorize(Roles = "ADMIN")]
		public ActionResult AtualizarRedacaoProfessor(Guid redacaoId)
		{
			try
			{
				var retorno = _redacaoService.VincularRedacaoProfessor(redacaoId, GetAspNetUserId());
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