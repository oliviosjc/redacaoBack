using Microsoft.Extensions.DependencyInjection;
using Redacao.Application.Services;
using Redacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.Services;
using Redacao.Avaliacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Data;
using Redacao.Avaliacao.Data.Repository;
using Redacao.Avaliacao.Domain.Repository;
using Redacao.Data.Repository;
using Redacao.Domain.Repository.Interface;
using Redacao.Log.Application.Services;
using Redacao.Log.Application.Services.Interface;
using Redacao.Log.Data;
using Redacao.Log.Data.Repository;
using Redacao.Log.Domain.Repository;
using Redacao.Usuario.Application.Services;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Data;
using Redacao.Usuario.Data.Repository;
using Redacao.Usuario.Domain.Repository;
using RedacaoData;
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
			services.AddTransient<RedacaoContext>();

			//usuario
			services.AddScoped<IUsuarioService, UsuarioService>();
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			services.AddScoped<IUsuarioCreditoService, UsuarioCreditoService>();
			services.AddScoped<IUsuarioCreditoRepository, UsuarioCreditoRepository>();
			services.AddTransient<UsuarioContext>();

			//avaliacao
			services.AddScoped<IAvaliacaoProfessorService, AvaliacaoProfessorService>();
			services.AddScoped<IAvaliacaoProfessorRepository, AvaliacaoProfessorRepository>();
			services.AddScoped<IAvaliacaoRedacaoService, AvaliacaoRedacaoService>();
			services.AddScoped<IAvaliacaoRedacaoRepository, AvaliacaoRedacaoRepository>();
			services.AddTransient<AvaliacaoContext>();

			//logger
			services.AddScoped<IRedacaoLogService, RedacaoLogService>();
			services.AddScoped<IRedacaoLogRepository, RedacaoLogRepository>();
			services.AddTransient<RedacaoLogContext>();

		}
	}
}
