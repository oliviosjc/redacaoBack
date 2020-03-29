using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Application.ViewModel
{
	public class AvaliacaoRedacaoViewModel
	{
		public AvaliacaoRedacaoViewModel()
		{

		}
		public Guid RedacaoId { get; set; }

		public Guid UsuarioAlunoId { get; set; }

		public int NotaCriterio01 { get; set; }

		public string AnotacaoCriterio01 { get; set; }

		public int NotaCriterio02 { get; set; }

		public string AnotacaoCriterio02 { get; set; }

		public int NotaCriterio03 { get; set; }

		public string AnotacaoCriterio03 { get; set; }

		public string PontosFortes { get; set; }

		public string PontosFracos { get; set; }

		public string Feedback { get; set; }
	}
}
