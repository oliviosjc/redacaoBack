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

		public ReturnRequestViewModel Atualizar(UsuarioViewModel usuario)
		{
			throw new NotImplementedException();
		}

		public DashboardUsuarioViewModel Dashboard(Guid usuarioId)
		{
			throw new NotImplementedException();
		}

		public ReturnRequestViewModel DesativarUsuario(Guid usuarioId)
		{
			throw new NotImplementedException();
		}

		public ReturnRequestViewModel DetalhesUsuario(Guid aspNetUserId)
		{
			throw new NotImplementedException();
		}

		public UsuarioViewModel DetalhesUsuarioById(Guid usuarioId)
		{
			throw new NotImplementedException();
		}

		public ReturnRequestViewModel ListarUsuarios()
		{
			throw new NotImplementedException();
		}

		public Task<ReturnRequestViewModel> Registrar(UsuarioViewModel usuario)
		{
			throw new NotImplementedException();
		}
	}
}
