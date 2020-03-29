using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Redacao.Log.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Data
{
	public class RedacaoLogContext
	{
		protected readonly IMongoDatabase _mongoDatabase;
		protected readonly MongoClient _client;
		protected readonly string _dbName;

		public RedacaoLogContext(IOptions<Settings> options)
		{
			_client = new MongoClient(options.Value.ConnectionString);

			if (_client != null)
				_mongoDatabase = _client.GetDatabase(options.Value.Database);

			_dbName = options.Value.Database;
		}


		public IMongoCollection<RedacaoLog> RedacaoLog
		{
			get
			{
				return _mongoDatabase.GetCollection<RedacaoLog>("RedacaoLog");
			}
		}
	}
}
