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

		public void Adicionar(string logLevel, string message, string action ,string json, Guid aspNetUserId)
		{
			var redacaoLog = new RedacaoLog
			{
				Id = new Guid(),
				LogLevel = logLevel,
				Message = message,
				Action = action,
				Json = json,
				CreatedTime = DateTime.Now,
				AspNetUserId = aspNetUserId
			};

			_repository.Adicionar(redacaoLog);
		}
	}
}
