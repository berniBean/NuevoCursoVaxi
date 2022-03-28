using aplicacion.Contratos;
using Dominio;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Seguridad.TokenSeguridad
{
    public class JwtGenerador : IJwtGenerador
    {
        public string CrearToken(Usuario usuario, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
            };

            if(roles!= null)
            {
                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("InstitutoYakurefu"));

            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(60),
                SigningCredentials = credentials
            };

            var TokenManejador = new JwtSecurityTokenHandler();
            var token = TokenManejador.CreateToken(tokenDescription);

            return TokenManejador.WriteToken(token);
        }
    }
}
