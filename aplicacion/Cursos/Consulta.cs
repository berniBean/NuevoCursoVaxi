using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistencia;


namespace aplicacion.Cursos
{
    public class Consulta
    {
        public class Ejecuta : EjecutaRespuestaGen<List<Curso>>
        {

        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta, List<Curso>>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<List<Curso>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                return await _context.Cursos.ToListAsync();
            }
        }
    }
}
