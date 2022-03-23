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

        [HttpGet]
        public async Task<ActionResult<List<InstructorDTO>>> GetInstructor()
        {
            return await Mediator.Send(new Consulta.Ejecuta());
        }
    }
}
