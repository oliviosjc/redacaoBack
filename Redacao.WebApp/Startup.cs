using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Redacao.Application.ViewModel;
using Redacao.Usuario.Data;
using Redacao.Avaliacao.Application.ViewModel;
using Redacao.Avaliacao.Data;
using Redacao.Log.Data;
using Redacao.WebApp.Setup;
using RedacaoData;

namespace Redacao.WebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "V1",
					Title = "Redação APP",
					Description = "Documentação dos endpoints."
				});

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme.",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});


			services.AddSwaggerGenNewtonsoftSupport();


			services.AddDbContext<RedacaoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
			services.AddDbContext<AvaliacaoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
			services.AddDbContext<UsuarioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

			services.Configure<Settings>(options =>
			{
				options.ConnectionString = Configuration.GetConnectionString("MongoDBConnection");
				options.Database = Configuration.GetConnectionString("MongoDBDataBase");
			});

			services.AddDefaultIdentity<Usuario.Domain.Entities.Usuario>(opt =>
			{
				opt.Password.RequiredLength = 10;
				opt.SignIn.RequireConfirmedEmail = true;
			})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<UsuarioContext>()
				.AddDefaultTokenProviders();

			//JWT
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = appSettings.ValidoEm,
					ValidIssuer = appSettings.Emissor
				};
			});

			services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));

			var config = new AutoMapper.MapperConfiguration(cfg =>
			{
				//REDACAO
				cfg.CreateMap<Redacao.Domain.Entities.Redacao, RedacaoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.Documento, DocumentoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.StatusRedacao, StatusRedacaoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.TemaRedacao, TemaRedacaoViewModel>();
				cfg.CreateMap<Redacao.Domain.Entities.TipoRedacao, TipoRedacaoViewModel>();

				//AVALIACAO
				cfg.CreateMap<Redacao.Avaliacao.Domain.Entities.AvaliacaoProfessor, AvaliacaoProfessorViewModel>();
				cfg.CreateMap<Redacao.Avaliacao.Domain.Entities.AvaliacaoRedacao, AvaliacaoRedacaoViewModel>();
			});

			IMapper mapper = config.CreateMapper();
			services.AddSingleton(mapper);

			services.RegisterServices();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Webmotors Stock API Swagger V1");
			});

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
