using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Domain.Entities
{
	public class AvaliacaoProfessor : Entity, IAggregateRoot
	{
		public AvaliacaoProfessor()
		{

		}
		public Guid UsuarioProfessorId { get; private set; }

		public Guid RedacaoId { get; private set; }
		
		public int QualidadeCorrecao { get; private set; }

		public string Observacao { get; private set; }

		public AvaliacaoProfessor(Guid usuarioProfessorId, Guid redacaoId, int qualidadeCorrecao, string observacao)
		{
			UsuarioProfessorId = usuarioProfessorId;
			RedacaoId = redacaoId;
			QualidadeCorrecao = qualidadeCorrecao;
			Observacao = observacao;
			Validar();
		}

		public void Validar()
		{
			Validacoes.ValidarSeNulo(UsuarioProfessorId, "O campo usuarioProfessorId não pode estar null.");
			Validacoes.ValidarSeNulo(RedacaoId, "O campo redacaoId não pode estar null.");
			Validacoes.ValidarSeNulo(QualidadeCorrecao, "O campo qualidadeCorrecao não pode estar null.");
		}

		public void AlterarObservacao(string observacao)
		{
			Validacoes.ValidarTamanho(observacao, 250 ,"O campo observacao não pode estar null.");
			Observacao = observacao;
		}

		public void AlterarQualidadeCorrecao(int qualidadeCorrecao)
		{
			Validacoes.ValidarSeNulo(qualidadeCorrecao, "O campo qualidadeCorrecao não pode estar null.");
		}
	}
}
