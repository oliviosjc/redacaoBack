using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.ViewModels
{
	public class DashboardUsuarioViewModel
	{
		public DashboardUsuarioViewModel()
		{

		}
		public int QuantidadePerguntas { get; set; }

		public int QuantidadeRedacoes { get; set; }

		public decimal MediaRedacoes { get; set; }
	}
}
