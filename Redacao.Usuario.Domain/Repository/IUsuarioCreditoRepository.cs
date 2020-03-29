using Redacao.Core.Data;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Repository
{
	public interface IUsuarioCreditoRepository
	{
		void Adicionar(UsuarioCredito usuarioCredito);

		void Atualizar(UsuarioCredito usuarioCredito);

		UsuarioCredito DetalhesUsuario(Guid usuarioId);
	}
}
