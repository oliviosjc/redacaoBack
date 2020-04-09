using Redacao.Domain.Entities;
using Redacao.Domain.Repository.Interface;
using Redacao.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Services
{
    public class RedacaoDomainService : IRedacaoDomainService
    {

        private readonly IRedacaoRepository _redacaoRepository;

        public RedacaoDomainService(IRedacaoRepository redacaoRepository)
        {
            _redacaoRepository = redacaoRepository;
        }
    }
}
