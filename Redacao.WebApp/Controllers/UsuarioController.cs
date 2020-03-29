using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Redacao.Core.DomainObjects;
using Redacao.Core.Enums;
using Redacao.Log.Application.Services.Interface;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModels;
using Redacao.WebApp.Auth;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {

        private readonly IUsuarioService _usuarioService;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IRedacaoLogService _log;

		public UsuarioController(IUsuarioService usuarioService, UserManager<IdentityUser> userManager, IRedacaoLogService log)
        {
            _usuarioService = usuarioService;
			_log = log;
        }

        [HttpGet]
		[Authorize(Roles = "ALUNO")]
        public ActionResult DetalhesUsuario()
        {
            try
            {
				var usuario = _usuarioService.DetalhesUsuario(GetAspNetUserId());
				_log.Adicionar("SUCESSO", "Listagem de detalhes do usuario", "DetalhesUsuario", "", GetAspNetUserId());
				return RetornoAPI(usuario);
            }
            catch(Exception ex)
            {
				return RetornoAPIException(ex);
			}
        }

		[HttpGet, Route("listar")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> ListarUsuarios()
        {
            try
            {
				var usuarios = _usuarioService.ListarUsuarios();
				return RetornoAPI(usuarios);
			}
			catch (Exception ex)
            {
				return RetornoAPIException(ex);
			}
		}

		[HttpPut]
		[Authorize(Roles = "ADMIN")]
		public ActionResult AtualizarUsuario(UsuarioViewModel model)
		{
			try
			{
				var retorno = _usuarioService.Atualizar(model);
				_log.Adicionar("SUCESSO", "Atualização de usuário com sucesso.", "AtualizarUsuario", "", GetAspNetUserId());
				return RetornoAPI(retorno);
			}
			catch(Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpGet, Route("dashboard")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult Dashboard(Guid usuarioId)
		{
			try
			{
				var retorno = _usuarioService.Dashboard(usuarioId);
				return Ok(retorno);
			}
			catch(EntityException ex)
			{
				return BadRequest(ex.Message);
			}
		}
    }
}