using aplicacion.Cursos;
using aplicacion.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace webapi.Controllers
{
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : MiControllerBase
    {

        [HttpGet]
        
        public async Task<ActionResult<List<CursoDTO>>> GetCursos()
        {
            return await Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> GetCursoId(Guid id)
        {
            return await Mediator.Send(new ConsultaID.Ejecuta { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostNuevoCurso(Nuevo.Ejecuta data)
        {
            var res = await Mediator.Send(data);
            return res;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> PutEditarCurso(Guid id, Editar.Ejecuta data)
        {
            data.Idcurso = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCurso(Guid id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
        }
    }
}
