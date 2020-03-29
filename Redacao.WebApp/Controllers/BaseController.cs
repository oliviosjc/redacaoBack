using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redacao.Core.Models;
using Redacao.Log.Application.Services.Interface;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
		[ApiExplorerSettings(IgnoreApi = true)]
		[Authorize(Roles = "ALUNO, ADMIN")]
		public Guid GetAspNetUserId()
		{
			var userId = new Guid(this.User.Claims.ToList().FirstOrDefault(f => f.Type == "userId").Value);
			return userId;
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		public ActionResult RetornoAPI(ReturnRequestViewModel model)
		{
			var retorno = new ReturnRequestViewModel();
			retorno.Message = "Erro interno da aplicação.";

			switch (model.HttpCode)
			{
				case HttpStatusCode.OK:
					return Ok(model);

				case HttpStatusCode.NoContent:
					return NoContent();

				case HttpStatusCode.BadRequest:
					return BadRequest(model);

				case HttpStatusCode.InternalServerError:
					return StatusCode(500, model);

				default:
					return StatusCode(500, retorno);
			}
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		public ActionResult RetornoAPIException(Exception ex)
		{
			var retorno = new ReturnRequestViewModel();
			retorno.Message = ex.Message;
			return StatusCode(500, retorno);
		}
	}
}