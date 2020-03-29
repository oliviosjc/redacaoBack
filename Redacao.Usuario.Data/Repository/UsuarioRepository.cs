﻿using Microsoft.EntityFrameworkCore;
using Redacao.Core.Data;
using Redacao.Core.DomainObjects;
using Redacao.Usuario.Domain.Entities;
using Redacao.Usuario.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Usuario.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly UsuarioContext _context;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

		public void Registrar(Domain.Entities.Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
				_context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new EntityException("Ocorreu um erro ao salvar o Usuário na base de dados.");
            }
            
        }

        public void Atualizar(Domain.Entities.Usuario usuario)
        {
            try
            {
                _context.Usuario.Update(usuario);
                _context.SaveChanges();
                Dispose();
            }
            catch (Exception)
            {
                throw new EntityException("Ocorreu um erro ao atualizar o Usuário na base de dados.");
            }
        }

        public void DesativarUsuario(Guid usuarioId)
        {
            try
            {
                var usuario = _context.Usuario.AsNoTracking().FirstOrDefault(f => f.Id == usuarioId);
                usuario.Desativar();
                _context.Usuario.Update(usuario);
                _context.SaveChanges();
                Dispose();
            }
            catch (Exception)
            {
                throw new EntityException("Ocorreu um erro ao desativar Usuário na base de dados.");
            }
        }

        public Domain.Entities.Usuario DetalhesUsuario(Guid aspNetUserId)
        {
            try
            {
                var usuario = _context.Usuario.AsNoTracking()
                                        .Include(i => i.Atividades)
                                        .Include(i => i.ComoConheceu)
                                        .Where(wh => wh.AspNetUserId == aspNetUserId)
                                        .FirstOrDefault();

                return usuario;
            }
            catch (Exception ex)
            {
                throw new EntityException("Ocorreu um erro ao listar os detalhes do usuário na base de dados.");
            }

        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public ICollection<Domain.Entities.Usuario> ListarUsuarios()
        {
            try
            {
                var usuarios = _context.Usuario.AsNoTracking()
                                            .Include(i => i.ComoConheceu)
                                            .ToList();

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new EntityException("Ocorreu um erro ao listar os usuários na base de dados.");
            }
        }

		public bool ValidarEmail(string email)
		{
			var emailExiste = _context.Usuario.Select(sl => new { sl.Email }).Where(wh => wh.Email == email).FirstOrDefault();

			if (emailExiste != null)
				return true;

			return false;
		}

		public Domain.Entities.Usuario DetalhesUsuarioById(Guid usuarioId)
		{
			var usuario = _context.Usuario.AsNoTracking().Where(wh => wh.Id == usuarioId).FirstOrDefault();
			return usuario;
		}
	}
}