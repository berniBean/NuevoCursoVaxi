using aplicacion.Contratos;
using aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using persistencia;

namespace aplicacion.Seguridad
{
    public class UsuarioActualizar
    {
        public class Ejecuta : IRequest<UserDto>
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
            public object RolNombre { get; internal set; }
        }
        public class EjecutaValidator : AbstractValidator<Ejecuta>
        {
            public EjecutaValidator()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Handler : HandlerRequestBase,IRequestHandler<Ejecuta, UserDto>
        {
            private readonly UserManager<Dominio.Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IPasswordHasher<Dominio.Usuario> _passwordHasher;
            public Handler(cursosbasesContext context, UserManager<Dominio.Usuario> userManager, IJwtGenerador jwtGenerador, IPasswordHasher<Dominio.Usuario> passwordHasher) : base(context)
            {
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(request.UserName);
                if (usuario == null)
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "el usuario no existe" });

               var resultado= await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.UserName).AnyAsync();
                if (resultado)
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.InternalServerError, new { Mensaje = "este email pertenece a otro usuario" });

                usuario.NombreCompleto = request.Nombre;
                usuario.PasswordHash = _passwordHasher.HashPassword(usuario, request.Password);
                usuario.Email = request.Email;

                var resultadoUpdate =await _userManager.UpdateAsync(usuario);
                var resultadoRoles = await _userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);
                if (resultadoUpdate.Succeeded)
                {
                    return new UserDto
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        UserName = usuario.UserName,
                        Email = usuario.Email,
                        Token = _jwtGenerador.CrearToken(usuario, listaRoles)
                    };
                }


                throw new Exception("No se puede actualizar el usuario");


            }
        }
    }
}
