using aplicacion.ManejadorError;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace aplicacion.Seguridad
{
    public class ObtenerRolesUsuario 
    {
        public class Ejecuta : IRequest<List<string>>
        {
            public string UserName { get; set; }
        }
        public class Handler : IRequestHandler<Ejecuta, List<string>>
        {
            public readonly UserManager<Dominio.Usuario> _userManager;
            public readonly RoleManager<IdentityRole> _roleManager;

            public Handler(UserManager<Dominio.Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var usuario = await _userManager.FindByNameAsync(request.UserName);
                if (usuario == null)
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "el usuario no existe" });
                
                var listado = await _userManager.GetRolesAsync(usuario);
                
                return new List<string>( listado);
            }
        }
    }
   


}
