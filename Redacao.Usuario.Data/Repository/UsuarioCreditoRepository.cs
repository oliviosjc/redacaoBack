using Microsoft.EntityFrameworkCore;
using Redacao.Core.Data;
using Redacao.Core.DomainObjects;
using Redacao.Usuario.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redacao.Usuario.Data.Repository
{
	public class UsuarioCreditoRepository : IUsuarioCreditoRepository
	{
		private readonly UsuarioContext _context;

		public UsuarioCreditoRepository(UsuarioContext context)
		{
			_context = context;
		}
		public void Adicionar()
		{
			try
			{
			}
			catch(Exception ex)
			{
				throw new EntityException("Ocorreu um erro ao salvar o usuarioCredito na base de dados.");
			}
		}

		public void Atualizar()
		{
			try
			{
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro ao atualizar o usuarioCredito na base de dados.");
			}
		}

		public void DetalhesUsuario()
		{
			try
			{

			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro ao busacar o usuarioCredito na base de dados.");
			}
		}
	}
}
