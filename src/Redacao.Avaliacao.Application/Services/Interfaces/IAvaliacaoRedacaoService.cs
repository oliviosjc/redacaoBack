using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Application.Services.Interfaces
{
	public interface IAvaliacaoRedacaoService
	{
		ReturnRequestViewModel Adicionar(AvaliacaoRedacaoViewModel model);

		ReturnRequestViewModel Atualizar(AvaliacaoRedacaoViewModel model);

		AvaliacaoRedacaoViewModel AvaliacaoRedacao(Guid redacaoId);

		ICollection<AvaliacaoRedacaoViewModel> AvaliacaoesRedacoesUsuarioAluno(Guid alunoId);
	}
}
