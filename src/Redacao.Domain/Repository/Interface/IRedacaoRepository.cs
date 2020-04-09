using Redacao.Core.Data;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Repository.Interface
{
    public interface IRedacaoRepository : IRepository<Domain.Entities.Redacao>
    {

        void Adicionar(Domain.Entities.Redacao redacao);

        void Atualizar(Domain.Entities.Redacao redacao);

		void Deletar();

        ICollection<Domain.Entities.Redacao>RedacoesPorUsuario(Guid usuarioId);

        Domain.Entities.Redacao DetalhesRedacao(Guid redacaoId);

        ICollection<TemaRedacao> ObterTemasRedacao();

        ICollection<TipoRedacao> ObterTiposRedacao();

        TemaRedacao ObterTemaRedacao(Guid id);

        TipoRedacao ObterTipoRedacao(Guid id);
    }
}
