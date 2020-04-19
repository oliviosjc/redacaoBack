using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Redacao.Application.Services;
using Redacao.Application.Services.Interfaces;
using Redacao.Application.ViewModel;
using Redacao.Avaliacao.Application.Services;
using Redacao.Avaliacao.Application.Services.Interfaces;
using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Avaliacao.Data;
using Redacao.Avaliacao.Data.Repository;
using Redacao.Avaliacao.Domain.Repository;
using Redacao.Core.Services;
using Redacao.Data.Repository;
using Redacao.Domain.Repository.Interface;
using Redacao.Email.Application.Services;
using Redacao.Email.Application.Services.Interfaces;
using Redacao.Log.Application.Services;
using Redacao.Log.Application.Services.Interface;
using Redacao.Log.Data;
using Redacao.Log.Data.Repository;
using Redacao.Log.Domain.Repository;
using Redacao.Usuario.Application.Services;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Data;
using RedacaoData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Tests.Setup
{
	public class DependencySetupFixture
	{
		public DependencySetupFixture()
		{
			var serviceCollection = new ServiceCollection();

			// SQL CONTEXTS
			serviceCollection.AddDbContext<UsuarioContext>(options => options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB; Initial Catalog=RedacaoDB;Integrated Security=True"));
			serviceCollection.AddDbContext<RedacaoContext>(options => options.UseInMemoryDatabase(databaseName: "RedacaoTestDatabase"));
			serviceCollection.AddDbContext<AvaliacaoContext>(options => options.UseInMemoryDatabase(databaseName: "AvaliacaoTestDatabase"));

			// MONGO CONTEXTS 
			serviceCollection.Configure<Settings>(options =>
			{
				options.ConnectionString = "mongodb://localhost:27017";
				options.Database = "RedacaoAPP";
			});
			serviceCollection.AddTransient<RedacaoLogContext>();


			// SERVICES
			serviceCollection.AddTransient<IAuthService, AuthService>();
			serviceCollection.AddTransient<IUsuarioService, UsuarioService>();
			serviceCollection.AddTransient<IRedacaoService, RedacaoService>();
			serviceCollection.AddTransient<IDocumentoService, DocumentoService>();
			serviceCollection.AddTransient<IRedacaoLogService, RedacaoLogService>();
			serviceCollection.AddTransient<IEmailService, EmailService>();
			serviceCollection.AddTransient<IAvaliacaoProfessorService, AvaliacaoProfessorService>();
			serviceCollection.AddTransient<IAvaliacaoRedacaoService, AvaliacaoRedacaoService>();
			serviceCollection.AddTransient<ICoreServices, CoreServices>();

			// REPOSITORIES
			serviceCollection.AddTransient<IRedacaoRepository, RedacaoRepository>();
			serviceCollection.AddTransient<IDocumentoRepository, DocumentoRepository>();
			serviceCollection.AddTransient<IAvaliacaoProfessorRepository, AvaliacaoProfessorRepository>();
			serviceCollection.AddTransient<IAvaliacaoRedacaoRepository, AvaliacaoRedacaoRepository>();
			serviceCollection.AddTransient<IRedacaoLogRepository, RedacaoLogRepository>();

			// AUTO MAPPER
			var config = new AutoMapper.MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Redacao.Domain.Entities.Redacao, RedacaoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.Documento, DocumentoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.StatusRedacao, StatusRedacaoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.TemaRedacao, TemaRedacaoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.TipoRedacao, TipoRedacaoViewModel>();
				cfg.CreateMap<Redacao.Avaliacao.Domain.Entities.AvaliacaoProfessor, AvaliacaoProfessorViewModel>();
				cfg.CreateMap<Redacao.Avaliacao.Domain.Entities.AvaliacaoRedacao, AvaliacaoRedacaoViewModel>();
			});

			IMapper mapper = config.CreateMapper();
			serviceCollection.AddSingleton(mapper);


			// Identity
			serviceCollection.AddDefaultIdentity<Usuario.Domain.Entities.Usuario>(opt =>
			{
				opt.Password.RequiredLength = 10;
				opt.SignIn.RequireConfirmedEmail = true;
			})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<UsuarioContext>()
				.AddDefaultTokenProviders();

			ServiceProvider = serviceCollection.BuildServiceProvider();
		}

		public ServiceProvider ServiceProvider { get; private set; }
	}
}
