using aplicacion;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using persistencia;

namespace webapi.Controllers
{
    public class MostrarRoles
    {
        public class Ejecuta : IRequest<List<IdentityRole>>
        {

        }

        public class Handler : HandlerRequestBase,  IRequestHandler<Ejecuta, List<IdentityRole>>
        {
        


            public Handler(cursosbasesContext context) : base(context)
            {
            }

            public async Task<List<IdentityRole>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var roles = await _context.Roles.ToListAsync();

                return roles;

            }
        }
    }
}
