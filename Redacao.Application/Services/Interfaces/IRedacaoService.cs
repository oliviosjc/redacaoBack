using Redacao.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Application.Services.Interfaces
{
    public interface IRedacaoService
    {
        IEnumerable<RedacaoViewModel> RedacoesPorUsuario(Guid usuarioId);

        RedacaoViewModel DetalhesRedacao(Guid redacaoId);

        void AdicionarRedacao(RedacaoViewModel redacao);

        void AtualizarRedacao(RedacaoViewModel redacao);

        IEnumerable<TipoRedacaoViewModel> ObterTiposRedacao();

        IEnumerable<TemaRedacaoViewModel> ObterTemasRedacao();


    }
}
