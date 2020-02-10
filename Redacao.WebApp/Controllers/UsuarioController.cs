using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redacao.Usuario.Application.Services.Interfaces;

namespace Redacao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet, Route("detalhesUsuario/{usuarioId}")]
        public ActionResult DetalhesUsuario(Guid usuarioId)
        {
            try
            {
                var usuario = _usuarioService.DetalhesUsuario(usuarioId);
                return Ok(usuario);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet, Route("listarUsuarios")]
        public ActionResult ListarUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.ListarUsuarios();
                return Ok(usuarios);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}