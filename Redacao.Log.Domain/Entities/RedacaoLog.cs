using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Domain.Entities
{
	public class RedacaoLog
	{
		public Guid Id { get; set; }

		public Int32 Level { get; set; }

		public string Metodo { get; set; }

		public string Mensagem { get; set; }

		public string Json { get; set; }

		public DateTime? CreatedTime { get; set; }

		public Guid? UsuarioId { get; set; }
	}
}
