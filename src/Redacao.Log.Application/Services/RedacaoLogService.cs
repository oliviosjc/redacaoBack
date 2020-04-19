using Redacao.Core.Enums;
using Redacao.Log.Application.Services.Interface;
using Redacao.Log.Domain.Entities;
using Redacao.Log.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Application.Services
{
	public class RedacaoLogService : IRedacaoLogService
	{
		private readonly IRedacaoLogRepository _repository;
		public RedacaoLogService(IRedacaoLogRepository repository)
		{
			_repository = repository;
		}

		public void Adicionar(LogLevelEnum level, string metodo, string mensagem, string json, Guid? usuarioId)
		{
			var redacaoLog = new RedacaoLog
			{
				Id = Guid.NewGuid(),
				Level = (int)level,
				CreatedTime = DateTime.Now,
				Mensagem = mensagem,
				Metodo = metodo,
				Json = json,
				UsuarioId = usuarioId
			};

			_repository.Adicionar(redacaoLog);
		}
	}
}
