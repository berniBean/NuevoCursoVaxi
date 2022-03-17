using aplicacion.ManejadorError;
using MediatR;
using persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var find = await _context.Cursos.FindAsync(request.Id);

                if (find == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });
                    //throw new Exception("No se puede eliminar curso");


                 _context.Remove(find);
                var resultado = await _context.SaveChangesAsync();
                
                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("no se pudo eliminar el curso");



            }
        }
    }
}
