using aplicacion.DTO;
using aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Cursos
{
    public  class ConsultaID
    {
        public class Ejecuta : EjecutaRespuestaGen<CursoDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : HandlerRequestMapperBase, IRequestHandler<Ejecuta, CursoDTO>
        {
            public Handler(cursosbasesContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<CursoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Cursos.Include(x => x.Cursoinstructors)
                    .ThenInclude(y => y.IdinstructorNavigation).FirstOrDefaultAsync(a => a.Idcurso.Equals(request.Id));

                if (curso == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });

                var cursoDto = _mapper.Map<Curso, CursoDTO> (curso);
                return cursoDto;
            }
        }
    }
}
