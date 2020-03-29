using AutoMapper;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModels;
using Redacao.Usuario.Domain.Entities;
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
			var usuarioCredito = new UsuarioCredito(model.UsuarioId, model.DataExpiracaoPlano, model.QuantidadeRedacoesPlano, model.QuantidadePerguntasPlano, model.QuantidadeRedacoesAvulsas, model.QuantidadePerguntasAvulsas, model.Saldo);
			_repository.Adicionar(usuarioCredito);
		}

		public void Atualizar(UsuarioCreditoViewModel model)
		{
			var usuarioCredito = new UsuarioCredito(model.UsuarioId, model.DataExpiracaoPlano, model.QuantidadeRedacoesPlano, model.QuantidadePerguntasPlano, model.QuantidadeRedacoesAvulsas, model.QuantidadePerguntasAvulsas, model.Saldo);
			_repository.Atualizar(usuarioCredito);
		}

		public UsuarioCreditoViewModel DetalheUsuario(Guid usuarioId)
		{
			return _mapper.Map<UsuarioCreditoViewModel>(_repository.DetalhesUsuario(usuarioId));
		}
	}
}
