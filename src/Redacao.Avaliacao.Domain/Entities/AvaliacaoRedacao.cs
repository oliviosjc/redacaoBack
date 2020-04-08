using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Domain.Entities
{
	public class AvaliacaoRedacao : Entity, IAggregateRoot
	{
		public AvaliacaoRedacao()
		{

		}

		public Guid RedacaoId { get; private set; }

		public Guid UsuarioAlunoId { get; private set; }

		public Guid UsuarioProfessorId { get; private set; }

		public int NotaCriterio01 { get; private set; }

		public string AnotacaoCriterio01 { get; private set; }

		public int NotaCriterio02 { get; private set; }

		public string AnotacaoCriterio02 { get; private set; }

		public int NotaCriterio03 { get; private set; }

		public string AnotacaoCriterio03 { get; private set; }

		public string PontosFortes { get; private set; }

		public string PontosFracos { get; private set; }

		public string Feedback { get; private set; }

		public AvaliacaoRedacao(Guid redacaoId, Guid usuarioAlunoId, Guid usuarioProfessorId,int notaCriterio01, string anotacaoCriterio01, int notaCriterio02, string anotacaoCriterio02, int notaCriterio03, string anotacaoCriterio03, string pontosFortes, string pontosFracos, string feedback)
		{
			RedacaoId = redacaoId;
			UsuarioAlunoId = usuarioAlunoId;
			UsuarioProfessorId = usuarioProfessorId;
			NotaCriterio01 = notaCriterio01;
			AnotacaoCriterio01 = anotacaoCriterio01;
			NotaCriterio02 = notaCriterio02;
			AnotacaoCriterio02 = anotacaoCriterio02;
			NotaCriterio03 = notaCriterio03;
			AnotacaoCriterio03 = anotacaoCriterio03;
			PontosFortes = pontosFortes;
			PontosFracos = pontosFracos;
			Feedback = feedback;

			Validar();
		}

		public void Validar()
		{
			Validacoes.ValidarSeNulo(RedacaoId, "O campo redacaoId não pode ser null");
			Validacoes.ValidarSeNulo(UsuarioAlunoId, "O campo usuarioAlunoId não pode ser null");
			Validacoes.ValidarSeNulo(UsuarioProfessorId, "O campo usuarioProfessorId não pode ser null");
			Validacoes.ValidarSeNulo(NotaCriterio01, "O campo notaCriterio01 não pode ser null");
			Validacoes.ValidarSeNulo(NotaCriterio02, "O campo notaCriterio02 não pode ser null");
			Validacoes.ValidarSeNulo(NotaCriterio03, "O campo notaCriterio03 não pode ser null");
			Validacoes.ValidarMinimoMaximo(NotaCriterio01, 0, 5, "O campo notaCriterio01 deve ter minimo 0 e maximo 5");
			Validacoes.ValidarMinimoMaximo(NotaCriterio02, 0, 5, "O campo notaCriterio02 deve ter minimo 0 e maximo 5");
			Validacoes.ValidarMinimoMaximo(NotaCriterio03, 0, 5, "O campo notaCriterio03 deve ter minimo 0 e maximo 5");
			Validacoes.ValidarTamanho(AnotacaoCriterio01, 100, "O campo anotacaoCriterio01 deve ter tamanho maximo de 100 caracteres");
			Validacoes.ValidarTamanho(AnotacaoCriterio02, 100, "O campo anotacaoCriterio02 deve ter tamanho maximo de 100 caracteres");
			Validacoes.ValidarTamanho(AnotacaoCriterio03, 100, "O campo anotacaoCriterio03 deve ter tamanho maximo de 100 caracteres");
			Validacoes.ValidarTamanho(PontosFortes, 100, "O campo pontosFortes deve ter tamanho maximo de 100 caracteres");
			Validacoes.ValidarTamanho(PontosFracos, 100, "O campo pontosFracos deve ter tamanho maximo de 100 caracteres");
			Validacoes.ValidarTamanho(Feedback, 100, "O campo feedback deve ter tamanho maximo de 100 caracteres");
		}

		public void AlterarNotaCriterio01(int notaCriterio01)
		{
			NotaCriterio01 = notaCriterio01;
			Validacoes.ValidarSeNulo(NotaCriterio01, "O campo notaCriterio01 não pode ser null");
			Validacoes.ValidarMinimoMaximo(NotaCriterio01, 0, 5, "O campo notaCriterio01 deve ter minimo 0 e maximo 5");
		}

		public void AlterarNotaCriterio02(int notaCriterio02)
		{
			NotaCriterio02 = notaCriterio02;
			Validacoes.ValidarSeNulo(NotaCriterio02, "O campo notaCriterio02 não pode ser null");
			Validacoes.ValidarMinimoMaximo(NotaCriterio02, 0, 5, "O campo notaCriterio02 deve ter minimo 0 e maximo 5");
		}

		public void AlterarNotaCriterio03(int notaCriterio03)
		{
			NotaCriterio03 = notaCriterio03;
			Validacoes.ValidarSeNulo(NotaCriterio03, "O campo notaCriterio03 não pode ser null");
			Validacoes.ValidarMinimoMaximo(NotaCriterio03, 0, 5, "O campo notaCriterio03 deve ter minimo 0 e maximo 5");
		}

		public void AlterarAnotacaoCriterio01(string anotacaoCriterio01)
		{
			AnotacaoCriterio01 = anotacaoCriterio01;
			Validacoes.ValidarTamanho(AnotacaoCriterio01, 100, "O campo anotacaoCriterio01 deve ter tamanho maximo de 100 caracteres");
		}

		public void AlterarAnotacaoCriterio02(string anotacaoCriterio02)
		{
			AnotacaoCriterio02 = anotacaoCriterio02;
			Validacoes.ValidarTamanho(AnotacaoCriterio02, 100, "O campo anotacaoCriterio01 deve ter tamanho maximo de 100 caracteres");
		}

		public void AlterarAnotacaoCriterio03(string anotacaoCriterio03)
		{
			AnotacaoCriterio03 = anotacaoCriterio03;
			Validacoes.ValidarTamanho(AnotacaoCriterio03, 100, "O campo anotacaoCriterio01 deve ter tamanho maximo de 100 caracteres");
		}

		public void AlterarPontosFortes(string pontosFortes)
		{
			PontosFortes = pontosFortes;
			Validacoes.ValidarTamanho(PontosFortes, 100, "O campo pontosFortes deve ter tamanho maximo de 100 caracteres");
		}

		public void AlterarPontosFracos(string pontosFracos)
		{
			PontosFracos = pontosFracos;
			Validacoes.ValidarTamanho(PontosFracos, 100, "O campo pontosFortes deve ter tamanho maximo de 100 caracteres");
		}

		public void AlterarFeedback(string feedback)
		{
			Feedback = feedback;
			Validacoes.ValidarTamanho(Feedback, 100, "O campo pontosFortes deve ter tamanho maximo de 100 caracteres");
		}
	}
}
