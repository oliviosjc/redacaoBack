using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Email.Application.ViewModel
{
	public class SendEmailViewModel
	{
		public string Destination { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}
