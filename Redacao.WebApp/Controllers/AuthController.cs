using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Redacao.Core.Enums;
using Redacao.Email.Application.Services.Interfaces;
using Redacao.Email.Application.ViewModel;
using Redacao.Log.Application.Services.Interface;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModel;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("nova-conta")]
		public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
		{
			try
			{
				var retorno = await _authService.RegistrarUsuario(registerUser);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPost("recuperar-senha")]
		public async Task<ActionResult> RecuperarSenha(ForgotPasswordViewModel model)
		{
			try
			{
				var retorno = await _authService.RecuperarSenha(model);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpPost("entrar")]
		public async Task<ActionResult> Login(LoginUserViewModel model)
		{
			try
			{
				var retorno = await _authService.Entrar(model);
				return RetornoAPI(retorno);
			}
			catch(Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}

		[HttpDelete, Route("{usuarioId}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> DesativarUsuario(Guid usuarioId)
		{
			try
			{
				var retorno = await _authService.DesativarUsuario(usuarioId);
				return RetornoAPI(retorno);
			}
			catch (Exception ex)
			{
				return RetornoAPIException(ex);
			}
		}
	}
}