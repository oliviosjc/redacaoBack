using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Domain.Entities
{
	public class RedacaoLog
	{
		public Guid Id { get; set; }

		public string LogLevel { get; set; }

		public string Action { get; set; }

		public string Message { get; set; }

		public string Json { get; set; }

		public DateTime? CreatedTime { get; set; }

		public Guid AspNetUserId { get; set; }
	}
}
