using Redacao.Log.Domain.Entities;
using Redacao.Log.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Data.Repository
{
	public class RedacaoLogRepository : IRedacaoLogRepository
	{
		private readonly RedacaoLogContext _mongoContext;

		public RedacaoLogRepository(RedacaoLogContext mongoContext)
		{
			_mongoContext = mongoContext;
		}

		public void Adicionar(RedacaoLog model)
		{
			try
			{
				_mongoContext.RedacaoLog.InsertOne(model);
			}
			catch(Exception ex)
			{
				throw new Exception("Ocorreu um erro erro ao adicionar log.");
			}
		}
	}
}
