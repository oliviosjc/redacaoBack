using Microsoft.Extensions.DependencyInjection;
using Moq;
using Redacao.Tests.Setup;
using Redacao.Usuario.Application.Services.Interfaces;
using Redacao.Usuario.Application.ViewModel;
using Redacao.Usuario.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Redacao.Tests
{
	public class UsuarioTest : IClassFixture<DependencySetupFixture>
	{
		private ServiceProvider _serviceProvider;

		public UsuarioTest(DependencySetupFixture fixture)
		{
			_serviceProvider = fixture.ServiceProvider;
		}


		[Fact]
		public async void AdicionarUsuarioComum()
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var context = scope.ServiceProvider.GetServices<UsuarioContext>();
				var authService = scope.ServiceProvider.GetService<IAuthService>();

				var usuario = new RegisterUserViewModel
				{
					Nome = "Olivio R",
					Telefone  = "12992084567",
					Genero = "Masculino",
					Password = "TesteSenha@123",
					ConfirmPassword = "TesteSenha@123",
					CPF = "380.954.208-32",
					Email = "emailteste1@hotmail.com"
				};

				var request = await authService.RegistrarUsuario(usuario);

				Assert.Equal(HttpStatusCode.OK, request.HttpCode);
			}
		}
	}
}
