
using aplicacion.ManejadorError;
using MediatR;
using persistencia;
using System.Net;

namespace aplicacion.Instructor
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid Idinstructor { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Grado { get; set; }
            public string Foto { get; set; }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var inst = await _context.Instructors.FindAsync(request.Idinstructor);

                if (inst == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { instructor = "No se encontró el instructor" });
                
                inst.Nombre = request.Nombre ?? inst.Nombre;
                inst.Apellidos = request.Apellidos ?? inst.Apellidos;
                inst.Grado = request.Grado ?? inst.Grado;
                inst.Foto = request.Foto ?? inst.Foto;

                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("No se guararon los cabios");
            }
        }
    }

}
