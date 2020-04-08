using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {

        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string CPF { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Genero { get; set; }

        public Guid TipoUsuarioId { get; set; }

        public TipoUsuarioViewModel TipoUsuario { get; set; }

        public Guid ComoConheceuId { get; set; }

        public ComoConheceuViewModel ComoConheceu { get; set; }

        public ICollection<AtividadesViewModel> Atividades { get; set; }

		public bool Ativo { get; set; }

		public Guid AspNetUserId { get; set; }
    }
}
