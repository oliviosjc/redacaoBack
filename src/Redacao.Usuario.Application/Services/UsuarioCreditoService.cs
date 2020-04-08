using AutoMapper;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModels;
using Redacao.Usuario.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.Services
{
	public class UsuarioCreditoService : IUsuarioCreditoService
	{

		private readonly IUsuarioCreditoRepository _repository;
		private readonly IMapper _mapper;

		public UsuarioCreditoService(IUsuarioCreditoRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}	

		public void Adicionar(UsuarioCreditoViewModel model)
		{
		}

		public void Atualizar(UsuarioCreditoViewModel model)
		{
		}

		public void DetalheUsuario(Guid usuarioId)
		{
		}
	}
}
