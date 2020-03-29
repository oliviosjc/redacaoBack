using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Redacao.Core.Models
{
	public class ReturnRequestViewModel
	{
		public HttpStatusCode HttpCode {get;set;}

		public string Message { get; set; }

		public dynamic Data { get; set; }
	}
}
