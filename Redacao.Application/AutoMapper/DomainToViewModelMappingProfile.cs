using AutoMapper;
using Redacao.Application.ViewModel;
using Redacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile  
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entities.Redacao, RedacaoViewModel>();
            CreateMap<Documento, DocumentoViewModel>();
            CreateMap<StatusRedacao, StatusRedacaoViewModel>();
            CreateMap<TemaRedacao, TemaRedacaoViewModel>();
            CreateMap<TipoRedacao, TipoRedacaoViewModel>();
        }
    }
}
