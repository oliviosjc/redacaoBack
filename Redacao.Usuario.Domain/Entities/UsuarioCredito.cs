using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Domain.Entities
{
	public class UsuarioCredito : IAggregateRoot
	{

		public UsuarioCredito()
		{

		}

		public Usuario Usuario { get; private set; }

		public Guid UsuarioId { get; private set; }

		public DateTime? DataExpiracaoPlano { get; private set; }

		public int QuantidadeRedacoesPlano { get; private set; }

		public int QuantidadePerguntasPlano { get; private set; }

		public int QuantidadeRedacoesAvulsas { get; private set; }

		public int QuantidadePerguntasAvulsas { get; private set; }

		public decimal Saldo { get; private set; }

		public UsuarioCredito(Guid usuarioId, DateTime? dataExpiracaoPlano, int qtdRedacaoPlano, int qtdPerguntaPlano, int qtdRedacaoAvulsa, int qtdPerguntaAvulsa, decimal saldo)
		{
			UsuarioId = usuarioId;
			DataExpiracaoPlano = dataExpiracaoPlano;
			QuantidadeRedacoesPlano = qtdRedacaoPlano;
			QuantidadePerguntasPlano = qtdPerguntaPlano;
			QuantidadeRedacoesAvulsas = qtdRedacaoAvulsa;
			QuantidadePerguntasAvulsas = qtdRedacaoAvulsa;
			Saldo = saldo;
		}

		public void AlterarDataExpiracaoPlano(DateTime? dataExpiracaoPlano)
		{
			DataExpiracaoPlano = dataExpiracaoPlano;
		}

		public void AlterarQuantidadeRedacoesPlano(int qtd)
		{
			QuantidadeRedacoesPlano = qtd;
		}

		public void AlterarQuantidadePerguntasPlano(int qtd)
		{
			QuantidadePerguntasPlano = qtd;
		}

		public void AlterarQuantidadeRedacoesAvulsas(int qtd)
		{
			QuantidadeRedacoesAvulsas = qtd;
		}

		public void AlterarQuantidadePerguntasAvulsas(int qtd)
		{
			QuantidadePerguntasAvulsas = qtd;
		}

		public void AlterarSaldo(decimal novoSaldo)
		{
			Saldo = novoSaldo;
		}
	}
}
