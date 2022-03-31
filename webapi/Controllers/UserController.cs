using aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UserController : MiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(Loggin.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UserDto>> Registrar(RegstrarUsuario.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpPut]
        public async Task<ActionResult<UserDto>> Actualizar(RegstrarUsuario.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> DevolverUsuario()
        {
            return await Mediator.Send(new UsuarioActual.Ejecuta());
        }

    }
}
