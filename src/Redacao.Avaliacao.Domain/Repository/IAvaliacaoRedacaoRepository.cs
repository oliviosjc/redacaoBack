using Redacao.Avaliacao.Domain.Entities;
using Redacao.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Domain.Repository
{
	public interface IAvaliacaoRedacaoRepository : IRepository<AvaliacaoRedacao>
	{
		void Adicionar(AvaliacaoRedacao avaliacaoRedacao);

		void Atualizar(AvaliacaoRedacao avaliacaoRedacao);

		AvaliacaoRedacao AvaliacaoRedacao(Guid redacaoId);

		ICollection<AvaliacaoRedacao> AvaliacoesRedacoesAluno(Guid alunoId);
	}
}
