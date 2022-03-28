using aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace aplicacion.Seguridad
{
    public class EliminarRolUsuario
    {
        public class Ejecuta : IRequest
        {
            public string UserName { get; set; }
            public string RolNombre { get; set; }
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Ejecuta>
        {
            public readonly UserManager<Dominio.Usuario> _userManager;
            public readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<Dominio.Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolNombre);
                if (role == null)
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "El rol no Existe" });

                var usuario = await _userManager.FindByNameAsync(request.UserName);
                if (usuario == null)
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "el usuario no existe" });

                var result = await _userManager.RemoveFromRoleAsync(usuario, request.RolNombre);
                if (result.Succeeded)
                    return Unit.Value;

                throw new Exception("No se pudo eliminar el rol al usuario");
            }
        }
    }
}
