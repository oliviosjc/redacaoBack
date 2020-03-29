using Microsoft.EntityFrameworkCore;
using Redacao.Core.Data;
using Redacao.Core.DomainObjects;
using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using RedacaoData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Redacao.Data.Repository
{
    public class RedacaoRepository : IRedacaoRepository
    {

        private readonly RedacaoContext _context;


        public RedacaoRepository(RedacaoContext context)
        {
            _context = context;
        }

        public void Adicionar(Domain.Entities.Redacao redacao)
        {
            try
            {
                _context.Redacao.Add(redacao);
                _context.SaveChanges();
            }
			catch (SqlException ex)
			{
				throw ex;
			}

		}

        public void Atualizar(Domain.Entities.Redacao r)
        {
            try
            {
                _context.Redacao.Update(r);
                _context.SaveChanges();
            }
			catch (SqlException ex)
			{
				throw ex;
			}

		}

		public void Deletar()
		{
			throw new NotImplementedException();
		}

		public Domain.Entities.Redacao DetalhesRedacao(Guid redacaoId)
        {
            try
            {
                var redacao = _context.Redacao.AsNoTracking()
                                          .Include(i => i.TemaRedacao)
                                          .Include(i => i.TipoRedacao)
                                          .Include(i => i.StatusRedacao)
                                          .Include(i => i.Documento)
                                          .Where(wh => wh.Id == redacaoId).FirstOrDefault();

                return redacao;
            }
            catch(SqlException ex)
            {
				throw ex;
            }
            
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public TemaRedacao ObterTemaRedacao(Guid id)
        {
			try
			{
				var tema = _context.TemaRedacao.FirstOrDefault(f => f.Id == id);
				return tema;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

        public ICollection<TemaRedacao> ObterTemasRedacao()
        {
			try
			{
				var temas = _context.TemaRedacao.AsNoTracking().ToList();
				return temas;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

        public TipoRedacao ObterTipoRedacao(Guid id)
        {
			try
			{
				var tipo = _context.TipoRedacao.FirstOrDefault(f => f.Id == id);
				return tipo;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

        public ICollection<TipoRedacao> ObterTiposRedacao()
        {
            var tipos = _context.TipoRedacao.AsNoTracking().ToList();
            return tipos;
        }

        public ICollection<Domain.Entities.Redacao> RedacoesPorUsuario(Guid usuarioId)
        {
            var redacoes = _context.Redacao.AsNoTracking()
                                          .Include(i => i.TemaRedacao)
                                          .Include(i => i.TipoRedacao)
                                          .Include(i => i.StatusRedacao)
                                          .Include(i => i.Documento)
                                          .Where(wh => wh.UsuarioAlunoId == usuarioId).ToList();

            return redacoes;
        }
    }
}
