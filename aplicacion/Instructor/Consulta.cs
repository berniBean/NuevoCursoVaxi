using aplicacion.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Instructor
{
    public class Consulta
    {
        public class Ejecuta : EjecutaRespuestaGen<List<InstructorDTO>>
        {

        }

        public class Handler : HandlerRequestMapperBase, IRequestHandler<Ejecuta, List<InstructorDTO>>
        {
            public Handler(cursosbasesContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<List<InstructorDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var res = await _context.Instructors.ToListAsync();

                var instDTO = _mapper.Map<List<Dominio.Instructor>, List<InstructorDTO>>(res);
                
                return instDTO;
            }
        }
    }
}
