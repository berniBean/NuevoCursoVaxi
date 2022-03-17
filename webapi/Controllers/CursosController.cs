using aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : MiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetCursos()
        {
            return await Mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCursoId(int id)
        {
            return await Mediator.Send(new ConsultaID.Ejecuta { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> PostNuevoCurso(Nuevo.Ejecuta data)
        {
            var res = await Mediator.Send(data);
            return res;
        }
        [HttpPut]
        public async Task<ActionResult<Unit>> PutEditarCurso(int id, Editar.Ejecuta data)
        {
            data.Idcurso = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteCurso(int id)
        {
            return await Mediator.Send(new Eliminar.Ejecuta { Id = id });
        }
    }
}
