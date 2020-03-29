using Microsoft.EntityFrameworkCore;
using Redacao.Core.Data;
using Redacao.Core.DomainObjects;
using Redacao.Usuario.Domain.Entities;
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
		public void Adicionar(UsuarioCredito usuarioCredito)
		{
			try
			{
				_context.UsuarioCredito.Add(usuarioCredito);
			}
			catch(Exception ex)
			{
				throw new EntityException("Ocorreu um erro ao salvar o usuarioCredito na base de dados.");
			}
		}

		public void Atualizar(UsuarioCredito usuarioCredito)
		{
			try
			{
				_context.UsuarioCredito.Update(usuarioCredito);
				_context.SaveChanges();
				_context.Dispose();
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro ao atualizar o usuarioCredito na base de dados.");
			}
		}

		public UsuarioCredito DetalhesUsuario(Guid usuarioId)
		{
			try
			{
				var usuarioCredito = _context.UsuarioCredito.AsNoTracking().FirstOrDefault(uc => uc.UsuarioId == usuarioId);
				return usuarioCredito;
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro ao busacar o usuarioCredito na base de dados.");
			}
		}
	}
}
