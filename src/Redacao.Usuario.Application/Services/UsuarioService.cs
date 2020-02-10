using AutoMapper;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModels;
using Redacao.Usuario.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public void Adicionar(UsuarioViewModel usuarioVM)
        {
            var usuario = new Domain.Entities.Usuario(usuarioVM.Nome, usuarioVM.Email, usuarioVM.Telefone, usuarioVM.CPF, usuarioVM.DataNascimento, usuarioVM.Genero, usuarioVM.TipoUsuarioId, usuarioVM.ComoConheceuId);
            usuario.Ativar();
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.UnitOfWork.Commit();
        }

        public void Atualizar(UsuarioViewModel usuario)
        {
            throw new NotImplementedException();
        }

        public void DesativarUsuario(Guid usuarioId)
        {
            var usuario = _usuarioRepository.DetalhesUsuario(usuarioId);
            usuario.Desativar();
            _usuarioRepository.UnitOfWork.Commit();
        }

        public UsuarioViewModel DetalhesUsuario(Guid usuarioId)
        {
            return _mapper.Map<UsuarioViewModel>(_usuarioRepository.DetalhesUsuario(usuarioId));
        }

        public ICollection<UsuarioViewModel> ListarUsuarios()
        {
            return _mapper.Map<ICollection<UsuarioViewModel>>(_usuarioRepository.ListarUsuarios());
        }
    }
}
