using aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace aplicacion.Seguridad
{
    public class RolEliminar
    {
        public class Ejecuta: IRequest
        {
            public string Nombre { get; set; }
        }

        public class EjecutaValida : AbstractValidator<Ejecuta>
        {
            public EjecutaValida()
            {
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var rol = await _roleManager.FindByNameAsync(request.Nombre);
                if(rol == null)
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No existe este rol" });

                var res = await _roleManager.DeleteAsync(rol);

                if (res.Succeeded)
                    return Unit.Value;

                throw new Exception("No se pudo eliminar el rol");
            }
        }
    }
}
