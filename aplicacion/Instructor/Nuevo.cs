using MediatR;
using persistencia;


namespace aplicacion.Instructor
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {            
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Grado { get; set; }            
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Guid _InstructorId = Guid.NewGuid();
                var inst = new Dominio.Instructor
                {
                    Idinstructor = _InstructorId,
                    Nombre = request.Nombre,
                    Apellidos = request.Apellidos,
                    Grado = request.Grado,
                };

                await _context.Instructors.AddAsync(inst);

                var res = await _context.SaveChangesAsync();

                if (res > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("no se pudo");
            }
        }
    }
}
