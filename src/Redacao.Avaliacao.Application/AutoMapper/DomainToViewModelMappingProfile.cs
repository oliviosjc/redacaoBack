using AutoMapper;
using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Avaliacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Avaliacao.Application.AutoMapper
{
	public class DomainToViewModelMappingProfile : Profile
	{
		public DomainToViewModelMappingProfile()
		{
			CreateMap<AvaliacaoProfessor, AvaliacaoProfessorViewModel>();
			CreateMap<AvaliacaoRedacao, AvaliacaoRedacaoViewModel>();
		}
	}
}
