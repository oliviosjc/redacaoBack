using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Application.ViewModel
{
	public class AvaliacaoProfessorViewModel
	{
		public Guid Id { get; set; }

		public Guid RedacaoId { get; set; }

		public Guid UsuarioProfessorId { get; set; }

		public Guid UsuarioAlunoId { get; set; }

		public int QualidadeCorrecao { get; set; }

		public string Observacao { get; set; }
	}
}
