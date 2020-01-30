using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Entities
{
    public class Redacao : Entity, IAggregateRoot
    {
        public string Descricao { get; private set; }

        public TipoRedacao TipoRedacao { get; private set; }

        public Guid TipoRedacaoId { get; private set; }

        public TemaRedacao TemaRedacao { get; private set; }

        public Guid TemaRedacaoId { get; private set; }

        public StatusRedacao StatusRedacao { get; private set; }

        public Guid StatusRedacaoId { get; private set; }

        public Documento Documento { get; private set; }

        public Guid DocumentoId { get; private set; }

        public Guid UsuarioAlunoId { get; private set; }

        public bool Ativo { get; private set; }

        public Redacao(string descricao, Guid tipoRedacaoId, Guid temaRedacaoId, Guid statusRedacaoId, Guid documentoId, Guid usuarioAlunoId, bool ativo)
        {
            Descricao = descricao;
            TipoRedacaoId = tipoRedacaoId;
            TemaRedacaoId = temaRedacaoId;
            StatusRedacaoId = statusRedacaoId;
            DocumentoId = documentoId;
            UsuarioAlunoId = usuarioAlunoId;
            Ativo = ativo;

            Validar();
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void AlterarTipoRedacao(TipoRedacao tipoRedacao)
        {
            TipoRedacao = tipoRedacao;
            TipoRedacaoId = tipoRedacao.Id;
        }

        public void AlterarTemaRedacao(TemaRedacao temaRedacao)
        {
            TemaRedacao = temaRedacao;
            TemaRedacaoId = temaRedacao.Id;
        }

        public void AlterarStatusRedacao(Guid statusRedacaoId)
        {
            StatusRedacaoId = statusRedacaoId;
        }

        public void AlterarDocumento(Documento documento)
        {
            Validacoes.ValidarSeNulo(documento, "O campo de documento da redação não pode ser alterado para nulo.");
            Documento = documento;
        }

        public void AlterarAluno(Guid usuarioAlunoId)
        {
            Validacoes.ValidarSeNulo(usuarioAlunoId, "O campo de usuarioAluno não pode ser alterado para nulo.");
            UsuarioAlunoId = usuarioAlunoId;
        }

        public void Validar()
        {
            Validacoes.ValidarTamanho(Descricao, 200, "O campo de descrição da redação não pode ser maior do que 200 caracteres");
            Validacoes.ValidarSeVazio(Descricao, "O campo descrição da redação não pode estar vazio.");
            Validacoes.ValidarSeNulo(TipoRedacaoId, "O campo tipo de radação não pode estar nulo.");
            Validacoes.ValidarSeNulo(TemaRedacaoId, "O campo tema da radação não pode estar nulo.");
            Validacoes.ValidarSeNulo(StatusRedacaoId, "O campo status da radação não pode estar nulo.");
            Validacoes.ValidarSeNulo(DocumentoId, "O campo documento não pode estar nulo.");
            Validacoes.ValidarSeNulo(UsuarioAlunoId, "O campo usuarioAlunoId não pode estar nulo.");
            Validacoes.ValidarSeNulo(Ativo, "O campo ativo não pode estar nulo.");
        }
    }
}
