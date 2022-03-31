using aplicacion.Comentario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    //[AllowAnonymous]
    [Route("api/[controller]")]
    public class ComentariosController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> PostNuevoComentario(Nuevo.Ejecuta data)
        {
            var res = await Mediator.Send(data);
            return res;
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Unit>>DeleteComentario(Guid id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>>PutActualizarComentario(Guid id, Editar.Ejecuta data)
        {
            data.Id = id;
            return await Mediator.Send(data);
        }

    }
}
