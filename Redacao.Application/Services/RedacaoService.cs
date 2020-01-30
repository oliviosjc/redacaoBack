using AutoMapper;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using static Redacao.Domain.Enums.RedacaoEnums;

namespace Redacao.Application.Services
{
    public class RedacaoService : IRedacaoService
    {
        private readonly IRedacaoRepository _redacaoRepository;
        private readonly IDocumentoService _documentoService;
        private readonly IMapper _mapper;

        public RedacaoService(IRedacaoRepository redacaoRepository, IMapper mapper, IDocumentoService documentoService)
        {
            _redacaoRepository = redacaoRepository;
            _mapper = mapper;
            _documentoService = documentoService;
        }

        public void AdicionarRedacao(RedacaoViewModel redacaoVM)
        {
            var documento = _documentoService.AdicionarDocumento(redacaoVM.Documento);
            var redacao = new Domain.Entities.Redacao(redacaoVM.Descricao, redacaoVM.TipoRedacaoId, 
                                                      redacaoVM.TemaRedacaoId, StatusRedacaoEnum.INICIADA,
                                                      documento.Id, redacaoVM.UsuarioAlunoId, true);

            _redacaoRepository.Adicionar(redacao);
            _redacaoRepository.UnitOfWork.Commit();
           
        }

        public void AtualizarRedacao(RedacaoViewModel redacaoVM)
        {
            _documentoService.AtualizarDocumento(redacaoVM.Documento);
            var redacao = _redacaoRepository.DetalhesRedacao(redacaoVM.Id);

            if (redacaoVM.Ativo)
                redacao.Ativar();
            redacao.Desativar();

            redacao.AlterarDescricao(redacaoVM.Descricao);
            redacao.AlterarAluno(redacaoVM.UsuarioAlunoId);

            var tipoRedacao = _redacaoRepository.ObterTipoRedacao(redacaoVM.TipoRedacaoId);
            var temaRedacao = _redacaoRepository.ObterTemaRedacao(redacaoVM.TemaRedacaoId);

            redacao.AlterarTipoRedacao(tipoRedacao);
            redacao.AlterarTemaRedacao(temaRedacao);

            _redacaoRepository.Atualizar(redacao);
            _redacaoRepository.UnitOfWork.Commit();
        }

        public RedacaoViewModel DetalhesRedacao(Guid redacaoId)
        {
            return _mapper.Map<RedacaoViewModel>(_redacaoRepository.DetalhesRedacao(redacaoId));
        }

        public IEnumerable<TemaRedacaoViewModel> ObterTemasRedacao()
        {
            return _mapper.Map<ICollection<TemaRedacaoViewModel>>(_redacaoRepository.ObterTemasRedacao());
        }

        public IEnumerable<TipoRedacaoViewModel> ObterTiposRedacao()
        {
            return _mapper.Map<ICollection<TipoRedacaoViewModel>>(_redacaoRepository.ObterTiposRedacao());
        }

        public IEnumerable<RedacaoViewModel> RedacoesPorUsuario(Guid usuarioId)
        {
            return _mapper.Map<ICollection<RedacaoViewModel>>(_redacaoRepository.RedacoesPorUsuario(usuarioId));
        }
    }
}
