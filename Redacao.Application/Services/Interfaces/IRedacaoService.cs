using Redacao.Application.ViewModel;
using Redacao.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Application.Services.Interfaces
{
    public interface IRedacaoService
    {
        ReturnRequestViewModel RedacoesPorUsuario(Guid usuarioId);

		ReturnRequestViewModel DetalhesRedacao(Guid redacaoId);

		ReturnRequestViewModel AdicionarRedacao(RedacaoViewModel redacao);

		ReturnRequestViewModel AtualizarRedacao(RedacaoViewModel redacao);

        ReturnRequestViewModel ObterTiposRedacao();

		ReturnRequestViewModel ObterTemasRedacao();

		ReturnRequestViewModel VincularRedacaoProfessor(Guid redacaoId, Guid professorId);
    }
}
