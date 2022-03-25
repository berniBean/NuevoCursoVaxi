using aplicacion.ManejadorError;
using MediatR;
using persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Comentario
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
            public string Alumno { get; set; }
            public int? Puntaje { get; set; }
            public string Comentario { get; set; }
            
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var editar = await _context.Comentarios.FindAsync(request.Id);


                if(editar == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el comentario" });

                editar.AlumnoName = request.Alumno  ?? editar.AlumnoName;
                editar.Comentario1 = request.Comentario ?? editar.Comentario1;
                editar.Puntaje = request.Puntaje ?? editar.Puntaje;

                var res = await _context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new Exception("No se guararon los cabios");



            }
        }
    }
}
