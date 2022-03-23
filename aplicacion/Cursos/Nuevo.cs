
using Dominio;
using FluentValidation;
using MediatR;
using persistencia;

namespace aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }

            public List<Guid> ListaInstructor { get; set; }
        }

        public class EjectaValidacion : AbstractValidator<Ejecuta>
        {
            public EjectaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Guid _cursoId = Guid.NewGuid();
                var curso = new Curso
                {
                    Idcurso = _cursoId,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };

                _context.Cursos.Add(curso);


                if(request.ListaInstructor != null)
                {
                    foreach (var item in request.ListaInstructor)
                    {
                        var Cursoinst = new Cursoinstructor
                        {
                            Idcurso = _cursoId,
                            Idinstructor = item
                        };
                        _context.Cursoinstructors.Add(Cursoinst);
                    }
                }


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
