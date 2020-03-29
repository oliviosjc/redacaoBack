using Microsoft.EntityFrameworkCore;
using Redacao.Avaliacao.Domain.Entities;
using Redacao.Avaliacao.Domain.Repository;
using Redacao.Core.Data;
using Redacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redacao.Avaliacao.Data.Repository
{
	public class AvaliacaoRedacaoRepository : IAvaliacaoRedacaoRepository
	{

		private readonly AvaliacaoContext _context;

		public AvaliacaoRedacaoRepository(AvaliacaoContext context)
		{
			_context = context;
		}

		public void Adicionar(AvaliacaoRedacao avaliacaoRedacao)
		{
			try
			{
				_context.AvaliacaoRedacao.Add(avaliacaoRedacao);
				_context.SaveChanges();
				_context.Dispose();
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dados ao inserir a avaliação da redacao.");
			}
		}

		public void Atualizar(AvaliacaoRedacao avaliacaoRedacao)
		{
			try
			{
				_context.AvaliacaoRedacao.Update(avaliacaoRedacao);
				_context.SaveChanges();
				_context.Dispose();
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dados ao atualizar a avaliação da redacao.");
			}
		}

		public AvaliacaoRedacao AvaliacaoRedacao(Guid redacaoId)
		{
			try
			{
				var avaliacaoRedacao = _context.AvaliacaoRedacao.AsNoTracking().Where(wh => wh.RedacaoId == redacaoId).FirstOrDefault();
				return avaliacaoRedacao;
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dados ao buscar a avaliação da redacao.");
			}
		}

		public ICollection<AvaliacaoRedacao> AvaliacoesRedacoesAluno(Guid alunoId)
		{
			try
			{
				var avaliacaoesRedacoesUsuarioAluno = _context.AvaliacaoRedacao.AsNoTracking().Where(wh => wh.UsuarioAlunoId == alunoId).ToList();
				return avaliacaoesRedacoesUsuarioAluno;
			}
			catch (Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dados ao buscar as avaliacoes por aluno.");
			}
		}

		public void Dispose()
		{
			_context?.Dispose();
		}
	}
}
