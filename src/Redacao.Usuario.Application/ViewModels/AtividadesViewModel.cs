using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.ViewModels
{
    public class AtividadesViewModel
    {
        public AtividadesViewModel()
        {

        }

        public Guid Id { get; set; }

        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public Guid UsuarioId { get; set; }
    }
}
