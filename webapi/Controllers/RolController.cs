using aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class RolController : MiControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> DeleteEliminaRol(RolEliminar.Ejecuta data)
        {
            
            return await Mediator.Send(data);
        }
        [HttpGet("ChocoRoles")]
        public async Task<ActionResult<List<IdentityRole>>> GetRoles()
        {
            return await Mediator.Send(new MostrarRoles.Ejecuta());
        }
        [HttpGet("userName")]
        public async Task<ActionResult<List<string>>> GetAllChocoRoles(string userName)
        {
            return await Mediator.Send(new ObtenerRolesUsuario.Ejecuta { UserName = userName});
        }
        [HttpPost("agregarChocoRolUsuario")]
        public async Task<ActionResult<Unit>> PutRol(AgregarRolUsuario.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpPost("EliminarChocoRolUsuario")]
        public async Task<ActionResult<Unit>> PostEliminarChocorol(EliminarRolUsuario.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>>PutActualizar(UsuarioActualizar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }

    }
}
