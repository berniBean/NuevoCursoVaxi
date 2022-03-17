using aplicacion.ManejadorError;
using MediatR;
using persistencia;
using System.Net;

namespace aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public int Idcurso { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Cursos.FindAsync(request.Idcurso);

                    if (curso == null)
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });

                curso.Nombre = request.Nombre ?? curso.Nombre;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                var resultado =  await _context.SaveChangesAsync();

                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("No se guararon los cabios");
            }
        }
    }
}
