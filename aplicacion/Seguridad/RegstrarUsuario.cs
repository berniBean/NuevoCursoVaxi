using aplicacion.Contratos;
using aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using persistencia;
using System.Net;

namespace aplicacion.Seguridad
{
    public class RegstrarUsuario
    {
        public class Ejecuta : EjecutaRespuestaGen<UserDto>
        {
            public string NombreCompleto { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class EJecutaValidador : AbstractValidator<Ejecuta>
        {
            public EJecutaValidador()
            {
                RuleFor(x => x.NombreCompleto).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : HandlerRequestBase, IRequestHandler<Ejecuta, UserDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Handler(cursosbasesContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador) : base(context)
            {
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }

            public async Task<UserDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                var UserNameExist = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                
                if (existe)
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El usauario ya existe" });
                
                if(UserNameExist)
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Este nombre de usuario ya fue registrado" });


                var usuario = new Usuario
                {
                    NombreCompleto = request.NombreCompleto,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var res =await _userManager.CreateAsync(usuario, request.Password);
                if (res.Succeeded)
                    return new UserDto
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        UserName = usuario.UserName
                    };

                throw new Exception("No se puede crear el nuevo usuario");

            }
        }
    }
}
