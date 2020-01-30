using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedacaoController : ControllerBase
    {

        private readonly IRedacaoService _redacaoService;

        public RedacaoController(IRedacaoService redacaoService)
        {
            _redacaoService = redacaoService;
        }

        [HttpGet, Route("redacoesPorUsuario/{usuarioId}")]
        public ActionResult ObterRedacoesPorUsuario(Guid usuarioId)
        {
            try
            {
                var redacoes = _redacaoService.RedacoesPorUsuario(usuarioId);
                return Ok(redacoes);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet, Route("detalhesRedacao/{id}")]
        public ActionResult ObterDetalhesDaRedacao(Guid id)
        {
            try
            {
                var redacao = _redacaoService.DetalhesRedacao(id);
                return Ok(redacao);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("novaRedacao")]
        public ActionResult AdicionarRedacao(RedacaoViewModel model)
        {
            try
            {
                _redacaoService.AdicionarRedacao(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("editarRedacao")]
        public ActionResult AtualizarRedacao(RedacaoViewModel model)
        {
            try
            {
                _redacaoService.AtualizarRedacao(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("tiposRedacao")]
        public ActionResult ObterTiposRedacao()
        {
            try
            {
                var tipos = _redacaoService.ObterTiposRedacao();
                return Ok(tipos);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet, Route("temasRedacao")]
        public ActionResult ObterTemasRedacao()
        {
            try
            {
                var temas = _redacaoService.ObterTemasRedacao();
                return Ok(temas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}