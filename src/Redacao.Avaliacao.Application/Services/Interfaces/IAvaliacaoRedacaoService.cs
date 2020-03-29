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

		ReturnRequestViewModel AvaliacaoRedacao(Guid redacaoId);

		ReturnRequestViewModel AvaliacaoesRedacoesUsuarioAluno(Guid alunoId);
	}
}
