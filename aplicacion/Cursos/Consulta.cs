using aplicacion.DTO;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistencia;


namespace aplicacion.Cursos
{
    public class Consulta
    {
        public class Ejecuta : EjecutaRespuestaGen<List<CursoDTO>>
        {

        }

        public class Handler : HandlerRequestMapperBase, IRequestHandler<Ejecuta, List<CursoDTO>>
        {
            public Handler(cursosbasesContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<List<CursoDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var res = await _context.Cursos.Include(x => x.Cursoinstructors)
                                    .ThenInclude(y => y.IdinstructorNavigation).ToListAsync();

                var CursosDTO = _mapper.Map<List<Curso>, List<CursoDTO>>(res);

                return CursosDTO;

            }
        }
    }
}
