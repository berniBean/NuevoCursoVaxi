using aplicacion.DTO;
using aplicacion.Instructor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> PostNuevoInstructor(Nuevo.Ejecuta data)
        {
            var res = await Mediator.Send(data);
            return res;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<InstructorDTO>>> GetInstructor()
        {
            return await Mediator.Send(new Consulta.Ejecuta());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDTO>> GetIdInstructor(Guid id)
        {
            return await Mediator.Send(new ConsultaID.Ejecuta { Id = id});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> PutEditarInstructor( Guid id, Editar.Ejecuta data)
        {
            data.Idinstructor = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteInstructor(Guid id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
        }


    }
}
