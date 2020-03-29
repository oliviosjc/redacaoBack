using Redacao.Avaliacao.Domain.Entities;
using Redacao.Core.Data;
using System;
using System.Collections.Generic;

namespace Redacao.Avaliacao.Domain.Repository
{
	public interface IAvaliacaoProfessorRepository : IRepository<AvaliacaoProfessor>
	{
		void Adicionar(AvaliacaoProfessor avaliacaoProfessor);

		void Atualizar(AvaliacaoProfessor avaliacaoProfessor);

		AvaliacaoProfessor ObterPorId(Guid redacaoId);

		ICollection<AvaliacaoProfessor> AvaliacoesPorProfessor(Guid professorId);
	}
}
