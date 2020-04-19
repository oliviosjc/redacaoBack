using Redacao.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Repository
{
	public interface IUsuarioCreditoRepository
	{
		void Adicionar();

		void Atualizar();

		void DetalhesUsuario();
	}
}
