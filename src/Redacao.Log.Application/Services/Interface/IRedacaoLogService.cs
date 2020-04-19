using Redacao.Core.Enums;
using Redacao.Log.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Application.Services.Interface
{
	public interface IRedacaoLogService
	{
		void Adicionar(LogLevelEnum level, string metodo ,string mensagem ,string json, Guid? usuarioId);
	}
}
