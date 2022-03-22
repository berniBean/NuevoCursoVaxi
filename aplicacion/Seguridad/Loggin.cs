using aplicacion.Contratos;
using aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace aplicacion.Seguridad
{
    public class Loggin
    {
        public class Ejecuta : EjecutaRespuestaGen<UserDto>
        {
            public string Emali { get; set;}
            public string Password { get; set; }
        }


        public class Handler :  IRequestHandler<Ejecuta, UserDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _singInManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Handler(UserManager<Usuario> userManager, SignInManager<Usuario> singInManager, IJwtGenerador jwtGenerador)
            {
                _userManager = userManager;
                _singInManager = singInManager;
                _jwtGenerador = jwtGenerador;
            }

            public class EjecutaValidacion : AbstractValidator<Ejecuta>
            {
                public EjecutaValidacion()
                {
                    RuleFor(x => x.Emali).NotEmpty().EmailAddress();
                    RuleFor(x => x.Password).NotEmpty();
                }
            }

            public async Task<UserDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var usuario = await _userManager.FindByEmailAsync(request.Emali);
                if (usuario == null)
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);

                var resultado = await _singInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                
                if (resultado.Succeeded)
                {
                    return new UserDto
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        Email = usuario.Email,
                        UserName = usuario.UserName
                        

                    };
                }
                    

                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
            }
        }
    }
}
