using AutoMapper;
using Redacao.Core.Models;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModels;
using Redacao.Usuario.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Application.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
		private readonly IUsuarioCreditoService _usuarioCreditoService;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IUsuarioCreditoService usuarioCreditoService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
			_usuarioCreditoService = usuarioCreditoService;
        }

        public Task<ReturnRequestViewModel> Registrar(UsuarioViewModel usuarioVM)
        {
			ReturnRequestViewModel retorno = new ReturnRequestViewModel();
            var usuario = new Domain.Entities.Usuario(usuarioVM.Nome, usuarioVM.Email, usuarioVM.Telefone, usuarioVM.CPF, usuarioVM.DataNascimento, usuarioVM.Genero,usuarioVM.ComoConheceuId, usuarioVM.AspNetUserId);
            usuario.Ativar();
			var validarEmail = _usuarioRepository.ValidarEmail(usuarioVM.Email);
			if(validarEmail)
			{
				retorno.Message = "O email já existe.";
				retorno.HttpCode = HttpStatusCode.BadRequest;

				return Task.FromResult(retorno);
			}
			_usuarioRepository.Registrar(usuario);

			var usuarioCreditoModel = new UsuarioCreditoViewModel
			{
				UsuarioId = usuario.Id,
				DataExpiracaoPlano = null,
				QuantidadePerguntasAvulsas = 0,
				QuantidadePerguntasPlano = 0,
				QuantidadeRedacoesAvulsas = 0,
				QuantidadeRedacoesPlano = 0,
				Saldo = 0
			};
			_usuarioCreditoService.Adicionar(usuarioCreditoModel);

			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Message = "O usuário foi adicionado com sucesso.";
			return Task.FromResult(retorno);
        }

        public ReturnRequestViewModel Atualizar(UsuarioViewModel usuario)
        {
			var retorno = new ReturnRequestViewModel();

			var usuarioEntidade = _usuarioRepository.DetalhesUsuarioById(usuario.Id);
			if (usuarioEntidade == null)
			{
				retorno.HttpCode = HttpStatusCode.BadRequest;
				retorno.Message = "O usuário de id " + usuario.Id + " não existe na base de dados.";
				return retorno;
			}

			if (!usuarioEntidade.Ativo)
			{
				retorno.HttpCode = HttpStatusCode.BadRequest;
				retorno.Message = "O usuário de id " + usuario.Id + " está inativo e por isso não pode ser atualizado.";
				return retorno;
			}

			if (usuarioEntidade.ComoConheceuId != usuario.ComoConheceuId)
				usuarioEntidade.AlterarComoConheceu(usuario.ComoConheceuId);

			if (usuarioEntidade.Genero != usuario.Genero)
				usuarioEntidade.AlterarGenero(usuario.Genero);

			if(usuarioEntidade.Telefone != usuario.Telefone)
				usuarioEntidade.AlterarTelefone(usuario.Telefone);

			_usuarioRepository.Atualizar(usuarioEntidade);
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Message = "Os dados do usuário foram atualizados com sucesso.";
			return retorno;
        }

		public DashboardUsuarioViewModel Dashboard(Guid usuarioId)
		{
			return new DashboardUsuarioViewModel();
		}

		public ReturnRequestViewModel DesativarUsuario(Guid usuarioId)
        {
			var retorno = new ReturnRequestViewModel();
			var usuario = _usuarioRepository.DetalhesUsuarioById(usuarioId);
			if (usuario == null)
			{
				retorno.Message = "O usuário de id " + usuarioId + " não existe.";
				retorno.HttpCode = HttpStatusCode.BadRequest;
			}

			if (!usuario.Ativo)
			{
				retorno.HttpCode = HttpStatusCode.BadRequest;
				retorno.Message = "O usuário de id " + usuario.Id + "já está desativado.";
				return retorno;
			}

			usuario.Desativar();
			_usuarioRepository.DesativarUsuario(usuarioId);
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Message = "O usuário foi desativado com sucesso.";
			return retorno;
        }

        public ReturnRequestViewModel DetalhesUsuario(Guid aspNetUserId)
        {
			var retorno = new ReturnRequestViewModel();
            var usuario =  _mapper.Map<UsuarioViewModel>(_usuarioRepository.DetalhesUsuario(aspNetUserId));

			if(usuario == null)
			{
				retorno.Message = "O usuário de id " + usuario.Id + " não foi encontrado na base de dados.";
				retorno.HttpCode = HttpStatusCode.NoContent;
			}

			retorno.Message = "O usuário de id " + usuario.Id + " foi encontrado na base de dados.";
			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Data = usuario;
			return retorno;
		}

        public ReturnRequestViewModel ListarUsuarios()
        {
			var retorno = new ReturnRequestViewModel();
            var usuarios = _mapper.Map<ICollection<UsuarioViewModel>>(_usuarioRepository.ListarUsuarios());

			if(usuarios == null || usuarios.Count == 0)
			{
				retorno.HttpCode = HttpStatusCode.NoContent;
				retorno.Message = "Nenhum usuário foi encontrado na base de dados.";
			}

			retorno.HttpCode = HttpStatusCode.OK;
			retorno.Message = "Os usuários foram encontrados na base de dados.";
			retorno.Data = usuarios;
			return retorno;
        }

		public UsuarioViewModel DetalhesUsuarioById(Guid usuarioId)
		{
			return _mapper.Map<UsuarioViewModel>(_usuarioRepository.DetalhesUsuarioById(usuarioId));
		}
	}
}
