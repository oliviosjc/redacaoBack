using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Redacao.Core.Enums;
using Redacao.Core.Models;
using Redacao.Core.Services;
using Redacao.Email.Application.Services.Interfaces;
using Redacao.Email.Application.ViewModel;
using Redacao.Log.Application.Services.Interface;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<Domain.Entities.Usuario> _userManager;
		private readonly SignInManager<Domain.Entities.Usuario> _signInManager;
		private readonly IEmailService _emailService;
		private readonly IRedacaoLogService _log;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ICoreServices _coreServices;
		private Guid? _usuarioLogadoId = new Guid();

		public AuthService(UserManager<Domain.Entities.Usuario> userManager,
						   SignInManager<Domain.Entities.Usuario> signInManager,
						   IEmailService emailService,
						   IRedacaoLogService log,
						   IHttpContextAccessor httpContextAccessor,
						   ICoreServices coreServices)
		{
			_userManager = userManager;
			_emailService = emailService;
			_signInManager = signInManager;
			_log = log;
			_httpContextAccessor = httpContextAccessor;
			_coreServices = coreServices;
			_usuarioLogadoId = _coreServices.GetLoggedUserId(_httpContextAccessor);
		}

		public async Task<ReturnRequestViewModel> DesativarUsuario(Guid usuarioId)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var usuario = await _userManager.FindByIdAsync(usuarioId.ToString());

				if (usuario == null)
				{
					retorno.Message = "O usuário não foi encontrado ou não existe.";
					retorno.HttpCode = HttpStatusCode.BadRequest;
					return retorno;
				}

				await _userManager.SetLockoutEndDateAsync(usuario, DateTime.Today.AddYears(10));

				retorno.Message = "O usuário foi desativado com sucesso!";
				retorno.HttpCode = HttpStatusCode.OK;

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(DesativarUsuario), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro intero ao desativar o usuário.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(DesativarUsuario), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		public async Task<ReturnRequestViewModel> Entrar(LoginUserViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

			if(result.IsNotAllowed)
			{
				retorno.Message = "O seu usuário ainda está desativado. Acesse o e-mail de confirmação de cadastro para ativa-lo.";
				retorno.HttpCode = HttpStatusCode.BadRequest;
				return retorno;
			}

			if (result.IsLockedOut)
			{
				retorno.Message = "O seu usuário foi impedido de entrar na plataforma. Contate o suporte para mais informações.";
				retorno.HttpCode = HttpStatusCode.BadRequest;
				return retorno;
			}

			if (!result.Succeeded)
			{
				retorno.Message = "Usuário ou senha incorretos.";
				retorno.HttpCode = HttpStatusCode.BadRequest;
				return retorno;
			}

			var token = await GerarJwt(model.Email);

			retorno.Message = "Login efetuado com sucesso!";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = token;
			return retorno;
		}

		public async Task<ReturnRequestViewModel> RegistrarUsuario(RegisterUserViewModel model)
		{
			var retorno = new ReturnRequestViewModel();
			
			try
			{
				var usuario = new Domain.Entities.Usuario
				{
					UserName = model.Email,
					Email = model.Email,
					EmailConfirmed = false,
					CPF = model.CPF,
					PhoneNumber = model.Telefone
				};

				var result = await _userManager.CreateAsync(usuario, model.Password);
				
				if (!result.Succeeded)
				{
					retorno.Message = result.Errors.Any() ? result.Errors.FirstOrDefault().Description : "Não foi possível registar o usuário. Tente novamente.";
					retorno.HttpCode = HttpStatusCode.BadRequest;
					return retorno;
				}

				await _userManager.AddToRoleAsync(usuario, "ADMIN");

				var token = await GerarJwt(usuario.Email);

				_emailService.EnviarEmailBoasVindas();

				retorno.Message = "Usuário cadastrado com sucesso.";
				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Data = token;

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(RegistrarUsuario), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao registrar o usuário.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(RegistrarUsuario), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		public async Task<ReturnRequestViewModel> ResetarSenha(ResetPasswordViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var usuario = await _userManager.FindByEmailAsync(model.Email);

				if (usuario == null)
				{
					retorno.Message = "O usuário não foi encontrado ou não existe.";
					retorno.HttpCode = HttpStatusCode.BadRequest;
					return retorno;
				}

				var resetarSenha = await _userManager.ResetPasswordAsync(usuario, model.Token, model.Password);

				if (!resetarSenha.Succeeded)
				{
					retorno.Message = resetarSenha.Errors.ToString();
					retorno.HttpCode = HttpStatusCode.BadRequest;
					return retorno;
				}

				retorno.Message = "A sua senha foi resetada com sucesso.";
				retorno.HttpCode = HttpStatusCode.OK;

				_emailService.EnviarEmailResetarSenha();

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(ResetarSenha), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao resetar a senha.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(ResetarSenha), retorno.Message, null, _usuarioLogadoId);
				return retorno;
			}
		}

		private async Task<string> GerarJwt(string email)
		{
			var usuario = await _userManager.FindByEmailAsync(email);

			var identityClaims = new ClaimsIdentity();

			identityClaims.AddClaims(await _userManager.GetClaimsAsync(usuario));

			var claims = new Claim[] { new Claim("userId", usuario.Id) }.ToList();

			var roles = await _userManager.GetRolesAsync(usuario);

			AddRolesToClaims(claims, roles);

			identityClaims.AddClaims(claims);

			var tokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes("SECRETREDACAOAPP");

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = identityClaims,
				Issuer = "REDACAOAPP",
				Audience = "http://localhost",
				Expires = DateTime.UtcNow.AddHours(4),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}

		private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
		{
			foreach (var role in roles)
			{
				var roleClaim = new Claim(ClaimTypes.Role, role);
				claims.Add(roleClaim);
			}
		}

		public async Task<ReturnRequestViewModel> RecuperarSenha(ForgotPasswordViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			var usuario = await _userManager.FindByEmailAsync(model.Email);

			if (usuario == null)
			{
				retorno.Message = "O e-mail informado está incorreto ou não existe.";
				retorno.HttpCode = HttpStatusCode.BadRequest;
				return retorno;
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

			var email = new SendEmailViewModel
			{
				Subject = "Recuperação de Senha",
				Destination = usuario.Email,
				Body = token
			};

			_emailService.EnviarEmailRecuperacaoSenha(email);

			retorno.Message = "E-mail de recuperação da senha enviado com sucesso. Verifique a pasta SPAM do seu e-mail!";
			retorno.HttpCode = HttpStatusCode.OK;
			return retorno;
		}
	}
}
