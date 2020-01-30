using Microsoft.Extensions.DependencyInjection;
using Redacao.Application.Services;
using Redacao.Application.Services.Interfaces;
using Redacao.Data.Repository;
using Redacao.Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redacao.WebApp.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //redacao
            services.AddScoped<IRedacaoService, RedacaoService>();
            services.AddScoped<IDocumentoService, DocumentoService>();
            services.AddScoped<IRedacaoRepository, RedacaoRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
        }
    }
}
