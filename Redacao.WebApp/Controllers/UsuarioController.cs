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
using Newtonsoft.Json;
using Redacao.Core.DomainObjects;
using Redacao.Core.Enums;
using Redacao.Log.Application.Services.Interface;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModel;
using Redacao.WebApp.Auth;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {

		private readonly IUsuarioService _service;
		public UsuarioController(IUsuarioService service)
        {
			_service = service;
        }

        [HttpGet]
		[Authorize(Roles = "ALUNO")]
        public ActionResult DetalhesUsuario()
        {
            try
            {
				var retorno = _service.DetalhesUsuario(GetAspNetUserId());
				return RetornoAPI(retorno.Result);
			}
			catch (Exception ex)
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
				var retorno = _service.ListarUsuarios();
				return RetornoAPI(retorno.Result);
			}
			catch (Exception ex)
            {
				return RetornoAPIException(ex);
			}
		}

		[HttpPut]
		[Authorize(Roles = "ADMIN")]
		public ActionResult AtualizarUsuario(UsuarioViewModel usuario)
		{
			try
			{
				var retorno = _service.AtualizarUsuario(usuario);
				return RetornoAPI(retorno.Result);
			}
			catch(Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpGet, Route("dashboard")]
		[Authorize(Roles = "ALUNO")]
		public ActionResult Dashboard()
		{
			try
			{
				return Ok();
			}
			catch (EntityException ex)
			{
				return BadRequest(ex.Message);
			}
		}
    }
}