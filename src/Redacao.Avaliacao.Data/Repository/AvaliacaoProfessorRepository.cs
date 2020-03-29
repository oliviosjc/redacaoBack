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
	public class AvaliacaoProfessorRepository : IAvaliacaoProfessorRepository
	{
		private readonly AvaliacaoContext _context;

		public AvaliacaoProfessorRepository(AvaliacaoContext context)
		{
			_context = context;
		}

		public void Adicionar(AvaliacaoProfessor avaliacaoProfessor)
		{
			try
			{
				_context.AvaliacaoProfessor.Add(avaliacaoProfessor);
				_context.SaveChanges();
				Dispose();
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dados ao inserir a avaliação do professor.");
			}
		}

		public void Atualizar(AvaliacaoProfessor avaliacaoProfessor)
		{
			try
			{
				_context.AvaliacaoProfessor.Update(avaliacaoProfessor);
				_context.SaveChanges();
				Dispose();
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dedos ao atualizar a avaliação do professor.");
			}
		}

		public ICollection<AvaliacaoProfessor> AvaliacoesPorProfessor(Guid professorId)
		{
			try
			{
				var avaliacoes = _context.AvaliacaoProfessor.AsNoTracking().Where(wh => wh.UsuarioProfessorId == professorId).ToList();
				return avaliacoes;
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dados ao listar as avaliacoes do professor.");
			}
		}

		public void Dispose()
		{
			_context?.Dispose();
		}

		public AvaliacaoProfessor ObterPorId(Guid redacaoId)
		{
			try
			{
				var avaliacaoProfessor = _context.AvaliacaoProfessor.AsNoTracking().FirstOrDefault(ap => ap.RedacaoId == redacaoId);
				return avaliacaoProfessor;
			}
			catch(Exception)
			{
				throw new EntityException("Ocorreu um erro na base de dados ao buscar a avaliacao.");
			}
		}
	}
}
