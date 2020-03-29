using Redacao.Log.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Application.Services.Interface
{
	public interface IRedacaoLogService
	{
		void Adicionar(string logLevel, string message,string action,string json, Guid aspNetUserId);
	}
}
