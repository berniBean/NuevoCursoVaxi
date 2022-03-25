using FluentValidation;
using MediatR;
using persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Comentario
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Alumno { get; set; }
            public int Puntaje { get; set; }
            public string Comentario { get; set; } 
            public Guid CursoId { get; set; }

        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Alumno).NotEmpty();
                RuleFor(x => x.Comentario).NotEmpty();
                RuleFor(x => x.Puntaje).NotEmpty();
                RuleFor(x => x.CursoId).NotEmpty();
            }
        }


        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var nuevoComentario = new Dominio.Comentario
                {
                    AlumnoName = request.Alumno,
                    Puntaje = request.Puntaje,
                    Comentario1 = request.Comentario,
                    CursoId = request.CursoId,
                    Idcomentario = Guid.NewGuid(),
                    FechaCreacion = DateTime.UtcNow

                };

                await _context.Comentarios.AddAsync(nuevoComentario);

                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("no se pudo realizar la inserción");
            }
        }
    }
}
