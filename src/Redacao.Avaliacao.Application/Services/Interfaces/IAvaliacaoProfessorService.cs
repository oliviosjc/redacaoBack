using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Application.Services.Interfaces
{
	public interface IAvaliacaoProfessorService
	{
		ReturnRequestViewModel Adicionar(AvaliacaoProfessorViewModel avaliacaoProfessor);

		ReturnRequestViewModel Atualizar(AvaliacaoProfessorViewModel avaliacaoProfessor);

		ReturnRequestViewModel AvaliacoesPorProfessor(Guid professorId);
	}
}
