using aplicacion.ManejadorError;
using Dominio;
using MediatR;
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
        public class Ejecuta : EjecutaRespuestaGen<Curso>
        {
            public int Id { get; set; }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta, Curso>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Curso> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Cursos.FindAsync(request.Id);
                if (curso == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });
                return curso;
            }
        }
    }
}
