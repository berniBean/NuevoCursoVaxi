using Dominio;
using MediatR;
using persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Cursos
{
    public  class ConsultaID
    {
        public class Ejecuta : EjecutaRespuestaGen<Curso>
        {
            public int Id { get; set; }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta, Curso>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Curso> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Cursos.FindAsync(request.Id);
                return curso;
            }
        }
    }
}
