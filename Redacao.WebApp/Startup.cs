using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Redacao.Application.ViewModel;
using Redacao.Usuario.Data;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<RedacaoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
            services.AddDbContext<UsuarioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

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

                //USUARIO
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
