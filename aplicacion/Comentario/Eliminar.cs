using aplicacion.ManejadorError;
using MediatR;
using persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Comentario
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id;
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var elminaComentario = await _context.Comentarios.FindAsync(request.Id);

                if(elminaComentario==null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el Comentario" });

                _context.Comentarios.Remove(elminaComentario);

                var res = await _context.SaveChangesAsync();

                if (res > 0)
                    return Unit.Value;

                throw new Exception("no se pudo eliminar el comentario");
            }
        }
    }
}
