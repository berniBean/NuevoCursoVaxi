using aplicacion.ManejadorError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistencia;
using System.Net;

namespace aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid Idcurso { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal? Precio { get; set; }
            public decimal? PrecioPromocion { get; set; }

            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> ListInstructor { get; set; }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta>
        {
            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Cursos.FindAsync(request.Idcurso);

                    if (curso == null)
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontró el curso" });

                curso.Nombre = request.Nombre ?? curso.Nombre;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                curso.FechaCreacion = DateTime.UtcNow;


                //actualizar precios 
                var precioEntidad = await _context.Precios.Where(x => x.CursoId.Equals(curso.Idcurso)).FirstOrDefaultAsync();
                if (precioEntidad != null)
                {
                    precioEntidad.Promocion = request.PrecioPromocion ?? precioEntidad.Promocion;
                    precioEntidad.PrecioActual = request.Precio ?? precioEntidad.PrecioActual;
                }
                else
                {
                    precioEntidad = new Dominio.Precio
                    {
                        PrecioId = Guid.NewGuid(),
                        PrecioActual = request.Precio ?? 0,
                        Promocion = request.PrecioPromocion ?? 0,
                        CursoId = curso.Idcurso
                        
                    };

                    await _context.Precios.AddAsync(precioEntidad);
                }


                if (request.ListInstructor != null)
                {
                    var instructores = await _context.Cursoinstructors.Where(x => x.Idcurso == request.Idcurso).ToListAsync();

                    foreach (var item in instructores)
                    {
                        _context.Cursoinstructors.Remove(item);
                    }
                }

                /*Agregar nuevos instructores*/
                foreach (var item in request.ListInstructor)
                {
                    var nuevoInst = new Dominio.Cursoinstructor
                    {
                        Idcurso = request.Idcurso,
                        Idinstructor = item
                       
                    };
                    _context.Cursoinstructors.Add(nuevoInst);
                }


                var resultado =  await _context.SaveChangesAsync();

                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("No se guararon los cabios");
            }
        }
    }
}
