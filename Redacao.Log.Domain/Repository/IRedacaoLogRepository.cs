using Redacao.Log.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Log.Domain.Repository
{
	public interface IRedacaoLogRepository
	{
		void Adicionar(RedacaoLog model);
	}
}
