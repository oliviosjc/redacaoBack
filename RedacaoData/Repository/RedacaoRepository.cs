using Microsoft.EntityFrameworkCore;
using Redacao.Core.Data;
using Redacao.Core.DomainObjects;
using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using RedacaoData;
using System;
using System.Collections.Generic;
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

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Domain.Entities.Redacao redacao)
        {
            try
            {
                _context.Redacao.Add(redacao);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw new EntityException("Ocorreu um erro ao salvar a redação na base de dados.");
            }
            
        }

        public void Atualizar(Domain.Entities.Redacao r)
        {
            try
            {
                _context.Redacao.Update(r);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw new EntityException("Ocorreu um erro ao atualizar a redação na base de dados.");
            }
            
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
            catch(Exception)
            {
                throw new EntityException("Ocorreu um erro ao buscar detalhes da redação. Tente novamente mais tarde.");
            }
            
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public TemaRedacao ObterTemaRedacao(Guid id)
        {
            var tema = _context.TemaRedacao.FirstOrDefault(f => f.Id == id);
            return tema;
        }

        public ICollection<TemaRedacao> ObterTemasRedacao()
        {
            var temas = _context.TemaRedacao.AsNoTracking().ToList();
            return temas;
        }

        public TipoRedacao ObterTipoRedacao(Guid id)
        {
            var tipo = _context.TipoRedacao.FirstOrDefault(f => f.Id == id);
            return tipo;
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
