using aplicacion.DTO;
using aplicacion.ManejadorError;
using AutoMapper;
using MediatR;
using persistencia;
using System.Net;

namespace aplicacion.Instructor
{
    public class ConsultaID
    {
        public class Ejecuta : IRequest<InstructorDTO>
        {
            public Guid Id;
        }


        public class Handler : HandlerRequestMapperBase, IRequestHandler<Ejecuta, InstructorDTO>
        {
            public Handler(cursosbasesContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<InstructorDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                {
                    var instructor = await _context.Instructors.FindAsync(request.Id);
                    if (instructor == null)
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el Instructor" });

                    var instDto = _mapper.Map<Dominio.Instructor, InstructorDTO>(instructor);
                    return instDto;
                }
            }
        }
    }
}
