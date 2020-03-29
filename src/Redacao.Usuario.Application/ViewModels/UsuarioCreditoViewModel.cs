using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.ViewModels
{
	public class UsuarioCreditoViewModel
	{

		public UsuarioCreditoViewModel()
		{

		}

		public Guid UsuarioId { get; set; }

		public UsuarioViewModel Usuario { get; set; }

		public DateTime? DataExpiracaoPlano { get; set; }

		public int QuantidadeRedacoesPlano { get; set; }

		public int QuantidadePerguntasPlano { get; set; }

		public int QuantidadeRedacoesAvulsas { get; set; }

		public int QuantidadePerguntasAvulsas { get; set; }

		public decimal Saldo { get; set; }
	}
}
