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
using Redacao.Core.Enums;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModels;
using Redacao.WebApp.InputModels;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {

		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly AppSettings _appSettings;
		private readonly IUsuarioService _usuarioService;

		public AuthController(SignInManager<IdentityUser> signInManager,
							  UserManager<IdentityUser> userManager,
							  IOptions<AppSettings> appSettings,
							  IUsuarioService usuarioService)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_appSettings = appSettings.Value;
			_usuarioService = usuarioService;
		}

		[HttpPost("nova-conta")]
		public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
		{
			var usuario = new UsuarioViewModel();
			usuario.AspNetUserId = Guid.NewGuid();
			usuario.Nome = registerUser.Nome;
			usuario.Email = registerUser.Email;
			usuario.ComoConheceuId = new Guid(ComoConheceuEnum.INTERNET);
			usuario.CPF = registerUser.CPF;
			usuario.DataNascimento = DateTime.Now;
			usuario.Genero = registerUser.Genero;
			usuario.Telefone = registerUser.Telefone;
			usuario.TipoUsuarioId = new Guid(TipoUsuarioEnum.COMUM);
			var retorno = await _usuarioService.Registrar(usuario);

			if (retorno.HttpCode != HttpStatusCode.OK)
				return BadRequest(retorno.Message);

			var user = new IdentityUser
			{
				Id = usuario.AspNetUserId.ToString(),
				UserName = registerUser.Email,
				Email = registerUser.Email,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, registerUser.Password);

			if (!result.Succeeded) return BadRequest(result.Errors);

			await _userManager.AddToRoleAsync(user, "ALUNO");

			await _signInManager.SignInAsync(user, false);
			return Ok(await GerarJwt(registerUser.Email));
		}

		[HttpPost("admin/nova-conta")]
		public async Task<ActionResult> AdminRegistrar(RegisterUserViewModel registerUser)
		{
			var usuario = new UsuarioViewModel();
			usuario.AspNetUserId = Guid.NewGuid();
			usuario.Nome = registerUser.Nome;
			usuario.Email = registerUser.Email;
			usuario.ComoConheceuId = new Guid(ComoConheceuEnum.INTERNET);
			usuario.CPF = registerUser.CPF;
			usuario.DataNascimento = DateTime.Now;
			usuario.Genero = registerUser.Genero;
			usuario.Telefone = registerUser.Telefone;
			usuario.TipoUsuarioId = new Guid(TipoUsuarioEnum.COMUM);
			var retorno = await _usuarioService.Registrar(usuario);

			if (retorno.HttpCode != HttpStatusCode.OK)
				return BadRequest(retorno.Message);

			var user = new IdentityUser
			{
				Id = usuario.AspNetUserId.ToString(),
				UserName = registerUser.Email,
				Email = registerUser.Email,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, registerUser.Password);

			if (!result.Succeeded) return BadRequest(result.Errors);

			await _userManager.AddToRoleAsync(user, "COMUM");

			await _signInManager.SignInAsync(user, false);
			return Ok(await GerarJwt(registerUser.Email));
		}

		[HttpPost("entrar")]
		public async Task<ActionResult> Login(LoginUserViewModel loginUser)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

			var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

			if (result.Succeeded)
			{
				return Ok(await GerarJwt(loginUser.Email));
			}

			return BadRequest("Usuário ou senha inválidos");
		}

		[HttpPost("recuperar-senha")]
		public async Task<ActionResult> RecuperarSenha(ForgotPasswordViewModel model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user == null)
			{
				return BadRequest();
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);


			//enviar email.

			return Ok();
		}

		[HttpPost, Route("resetar-senha")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetarSenha(ResetPasswordViewModel resetPasswordModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
			if (user == null)
				return BadRequest();

			var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
			if (!resetPassResult.Succeeded)
			{
				foreach (var error in resetPassResult.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
			}

			return Ok();
		}

		[HttpDelete, Route("{usuarioId}")]
		//[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> DesativarUsuario(Guid usuarioId)
		{
			try
			{
				var usuario = _usuarioService.DetalhesUsuarioById(usuarioId);
				var user = await _userManager.FindByIdAsync(usuario.AspNetUserId.ToString());
				await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(10));

				var retorno = _usuarioService.DesativarUsuario(usuarioId);
				return Ok(retorno.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[ApiExplorerSettings(IgnoreApi=true)]
		private async Task<string> GerarJwt(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			var identityClaims = new ClaimsIdentity();
			identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));
			var claims = new Claim[]
			{
				new Claim("userId", user.Id)
			}.ToList();

			var roles = await _userManager.GetRolesAsync(user);
			AddRolesToClaims(claims, roles);
			identityClaims.AddClaims(claims);
			// authentication successful so generate jwt token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = identityClaims,
				Issuer = _appSettings.Emissor,
				Audience = _appSettings.ValidoEm,
				Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
		{
			foreach (var role in roles)
			{
				var roleClaim = new Claim(ClaimTypes.Role, role);
				claims.Add(roleClaim);
			}
		}
	}
}