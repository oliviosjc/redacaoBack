using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Redacao.Core.Enums;
using Redacao.Core.Models;
using Redacao.Core.Services;
using Redacao.Email.Application.Services.Interfaces;
using Redacao.Log.Application.Services.Interface;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModel;
using Redacao.Usuario.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Application.Services
{
	public class UsuarioService : IUsuarioService
	{
		private readonly UserManager<Domain.Entities.Usuario> _userManager;
		private readonly IEmailService _emailService;
		private readonly UsuarioContext _context;
		private readonly IRedacaoLogService _log;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ICoreServices _coreServices;
		private Guid? _usuarioLogadoId = new Guid();

		public UsuarioService(IEmailService emailService, 
							  UsuarioContext context,
							  IRedacaoLogService log,
							  IHttpContextAccessor httpContextAccessor,
							  ICoreServices coreServices)
		{
			_emailService = emailService;
			_context = context;
			_log = log;
			_httpContextAccessor = httpContextAccessor;
			_coreServices = coreServices;
			_usuarioLogadoId = _coreServices.GetLoggedUserId(_httpContextAccessor);
		}

		public Task<ReturnRequestViewModel> AtualizarUsuario(UsuarioViewModel model)
		{
			var retorno = new ReturnRequestViewModel();

			try
			{
				var usuario = _userManager.FindByIdAsync(model.Id.ToString()).Result;
				if (usuario == null)
				{
					retorno.Message = "Não foi encontrado nenhum usuário na base de dados.";
					retorno.HttpCode = HttpStatusCode.NoContent;
					return Task.FromResult(retorno);
				}

				if(usuario.CPF == null)
				{
					usuario.CPF = model.CPF;
				}

				usuario.PhoneNumber = model.Telefone;
				usuario.UserName = model.Nome;

				var update = _userManager.UpdateAsync(usuario).Result;
				if(!update.Succeeded)
				{
					retorno.Message = "Ocorreu um erro intero ao atualizar as informações do usuário.";
					retorno.HttpCode = HttpStatusCode.InternalServerError;

					_log.Adicionar(LogLevelEnum.SUCCESS, nameof(AtualizarUsuario), retorno.Message, null, _usuarioLogadoId);
					return Task.FromResult(retorno);
				}

				retorno.Message = "Usuário atualizado com sucesso.";
				retorno.HttpCode = HttpStatusCode.OK;

				_log.Adicionar(LogLevelEnum.SUCCESS, nameof(AtualizarUsuario), retorno.Message, null, _usuarioLogadoId);
				return Task.FromResult(retorno);
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro intero ao atualizar as informações do usuário.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;

				_log.Adicionar(LogLevelEnum.ERROR, nameof(AtualizarUsuario), retorno.Message, null, _usuarioLogadoId);
				return Task.FromResult(retorno);
			}
		}

		public Task<ReturnRequestViewModel> DetalhesUsuario(Guid usuarioId)
		{
			var retorno = new ReturnRequestViewModel();
			try
			{
				var usuario =  _userManager.FindByIdAsync(usuarioId.ToString()).Result;
				if(usuario == null)
				{
					retorno.Message = "Não foi encontrado nenhum usuário na base de dados.";
					retorno.HttpCode = HttpStatusCode.NoContent;
					return Task.FromResult(retorno);
				}

				var usuarioVM = new UsuarioViewModel
				{
					Id = new Guid(usuario.Id),
					CPF = usuario.CPF,
					Email = usuario.Email,
					Nome = usuario.UserName,
					Telefone = usuario.PhoneNumber
				};

				retorno.Message = "O usuário foi encontrado.";
				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Data = usuarioVM;
				return Task.FromResult(retorno);
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao buscar o usuário na base de dados.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return Task.FromResult(retorno);
			}
		}

		public Task<ReturnRequestViewModel> ListarUsuarios()
		{
			var retorno = new ReturnRequestViewModel();
			try
			{
				var usuarios = _context.Users.Select(sl => new UsuarioViewModel
				{
					Id = new Guid(sl.Id),
					CPF = sl.CPF,
					Email = sl.Email,
					Nome = sl.UserName, 
					Telefone = sl.PhoneNumber
				}).ToList();

				retorno.Message = "Lista de usuários retornado com sucesso.";
				retorno.HttpCode = HttpStatusCode.OK;
				retorno.Data = usuarios;
				return Task.FromResult(retorno);
			}
			catch(Exception)
			{
				retorno.Message = "Ocorreu um erro interno ao listar os usuários.";
				retorno.HttpCode = HttpStatusCode.InternalServerError;
				return Task.FromResult(retorno);
			}
		}
	}
}
