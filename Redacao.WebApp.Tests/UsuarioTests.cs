using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Redacao.Core.Enums;
using Redacao.Usuario.Application.Services;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModels;
using Redacao.Usuario.Data;
using Redacao.Usuario.Data.Repository;
using Redacao.Usuario.Domain.Repository;
using Redacao.WebApp.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace Redacao.WebApp.Tests
{
	public class UsuarioTests
	{

		private IUsuarioService _usuarioService;

		public UsuarioTests()
		{
			var services = new ServiceCollection();
			services.RegisterServices();
			services.AddDbContext<UsuarioContext>(options => options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB; Initial Catalog=RedacaoDB;Integrated Security=True"));
			var config = new AutoMapper.MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Redacao.Usuario.Domain.Entities.Usuario, UsuarioViewModel>();
				cfg.CreateMap<Redacao.Usuario.Domain.Entities.UsuarioCredito, UsuarioViewModel>();
				cfg.CreateMap<Redacao.Usuario.Domain.Entities.TipoUsuario, TipoUsuarioViewModel>();
				cfg.CreateMap<Redacao.Usuario.Domain.Entities.Atividade, AtividadesViewModel>();
				cfg.CreateMap<Redacao.Usuario.Domain.Entities.ComoConheceu, ComoConheceuViewModel>();
				cfg.CreateMap<Redacao.Usuario.Domain.Entities.Atividade, AtividadesViewModel>();
			});

			IMapper mapper = config.CreateMapper();
			services.AddSingleton(mapper);
			var serviceProvider = services.BuildServiceProvider();
			_usuarioService = serviceProvider.GetService<IUsuarioService>();
		}

		[Fact]
		public void DetalhesUsuarioExistente()
		{
			var usuarioId = new Guid("F4B01E77-F305-4A17-A2C2-C36EF66A7F62");

			var usuario = _usuarioService.DetalhesUsuario(usuarioId);

			Assert.IsType<UsuarioViewModel>(usuario);
		}

		[Fact]
		public void DetalhesUsuarioNaoExistente()
		{
			var usuarioId = new Guid();

			var usuario = _usuarioService.DetalhesUsuario(usuarioId);

			Assert.IsType<UsuarioViewModel>(usuario);
		}

		[Fact]
		public void ListaUsuarios()
		{
			var usuarios = _usuarioService.ListarUsuarios().ToList();

			Assert.IsType<List<UsuarioViewModel>>(usuarios);
		}

		[Fact]
		public void AdicionarUsuarioCorreto()
		{

			var usuario = new UsuarioViewModel
			{
				ComoConheceuId = new Guid(ComoConheceuEnum.INTERNET),
				CPF = "38095420832",
				DataNascimento = new DateTime(),
				Email = "olivio_sjc@live.com",
				Nome = "Nome teste",
				Telefone = "1239393912",
				Genero = "Masculino",
				TipoUsuarioId = new Guid(TipoUsuarioEnum.COMUM)
			};

			var retorno = _usuarioService.Adicionar(usuario);
			Assert.Equal(HttpStatusCode.OK, retorno.HttpCode);
		}

		[Fact]
		public void AtualizarUsuarioCorreto()
		{
			var usuario = new UsuarioViewModel
			{
				Id = new Guid("F4B01E77-F305-4A17-A2C2-C36EF66A7F62"),
				ComoConheceuId = new Guid(ComoConheceuEnum.INTERNET),
				CPF = "38095420832",
				DataNascimento = new DateTime(1997, 2, 14),
				Email = "olivio_sjc@live.com",
				Genero = "Masculino",
				Nome = "OLIVIO RODRIGUES DA SILVA",
				Telefone = "12992084567",
				TipoUsuarioId = new Guid(TipoUsuarioEnum.COMUM)
			};

			var retorno = _usuarioService.Atualizar(usuario);

			Assert.Equal(HttpStatusCode.OK, retorno.HttpCode);
		}

		[Fact]
		public void AtualizarUsuarioIncorreto()
		{
			var usuario = new UsuarioViewModel
			{
				Id = new Guid(),
				ComoConheceuId = new Guid(ComoConheceuEnum.INTERNET),
				CPF = "38095420832",
				DataNascimento = new DateTime(1997, 2, 14),
				Email = "olivio_sjc@live.com",
				Genero = "Masculino",
				Nome = "OLIVIO RODRIGUES DA SILVA",
				Telefone = "12992084567",
				TipoUsuarioId = new Guid(TipoUsuarioEnum.COMUM)
			};

			var retorno = _usuarioService.Atualizar(usuario);

			Assert.Equal(HttpStatusCode.BadRequest, retorno.HttpCode);
		}

		[Fact]
		public void DeletarUsuarioCorreto()
		{
			var usuarioId = new Guid("F4B01E77-F305-4A17-A2C2-C36EF66A7F62");

			var retorno = _usuarioService.DesativarUsuario(usuarioId);

			Assert.Equal(HttpStatusCode.OK, retorno.HttpCode);
		}

		[Fact]
		public void DeletarUsuarioIncorreto()
		{
			var usuarioId = new Guid();

			var retorno = _usuarioService.DesativarUsuario(usuarioId);

			Assert.Equal(HttpStatusCode.BadRequest, retorno.HttpCode);
		}
	}
}
