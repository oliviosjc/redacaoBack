using AutoMapper;
using Redacao.Usuario.Application.ViewModels;
using Redacao.Usuario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Usuario.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entities.Usuario, UsuarioViewModel>();
            CreateMap<Atividade, AtividadesViewModel>();
            CreateMap<ComoConheceu, ComoConheceuViewModel>();
            CreateMap<TipoUsuario, TipoUsuarioViewModel>();
        }
    }
}
