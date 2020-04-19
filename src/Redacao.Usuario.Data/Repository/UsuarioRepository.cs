using Microsoft.EntityFrameworkCore;
using Redacao.Core.Data;
using Redacao.Core.DomainObjects;
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

		public void Atualizar()
		{
			throw new NotImplementedException();
		}

		public void DesativarUsuario(Guid usuarioId)
		{
			throw new NotImplementedException();
		}

		public void DetalhesUsuario()
		{
			throw new NotImplementedException();
		}

		public void DetalhesUsuarioById()
		{
			throw new NotImplementedException();
		}

		public void ListarUsuarios()
		{
			throw new NotImplementedException();
		}

		public void Registrar()
		{
			throw new NotImplementedException();
		}

		public bool ValidarEmail(string email)
		{
			throw new NotImplementedException();
		}
	}
}
