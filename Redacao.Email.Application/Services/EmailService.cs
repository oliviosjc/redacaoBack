using Redacao.Email.Application.Services.Interfaces;
using Redacao.Email.Application.ViewModel;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redacao.Email.Application.Services
{
	public class EmailService : IEmailService
	{
		public EmailService()
		{
		}

		public void EnviarEmailBoasVindas()
		{
		}

		public void EnviarEmailRecuperacaoSenha(SendEmailViewModel model)
		{
			//var apiKey = "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";
			//var client = new SendGridClient(apiKey);
			//var from = new EmailAddress("brayanmagalhaes@hotmail.com", "Clonado BRAYAN");
			//var subject = model.Subject;
			//var to = new EmailAddress("brayanmagalhaes0123@gmail.com", "Example User");
			//var msg = MailHelper.CreateSingleEmail(from, to, subject, model.Body, model.Body);
			//var response = client.SendEmailAsync(msg);
		}

		public void EnviarEmailResetarSenha()
		{
		}

		public void EnviarEmailUsuarioDesativado()
		{
		}
	}
}
