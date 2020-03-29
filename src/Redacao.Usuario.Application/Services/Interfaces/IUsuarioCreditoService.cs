using Redacao.Usuario.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.Services.Interfaces
{
	public interface IUsuarioCreditoService
	{
		void Adicionar(UsuarioCreditoViewModel usuarioCredito);

		void Atualizar(UsuarioCreditoViewModel usuarioCredito);

		UsuarioCreditoViewModel DetalheUsuario(Guid usuarioId);
	}
}
