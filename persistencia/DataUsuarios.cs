using Dominio;
using Microsoft.AspNetCore.Identity;


namespace persistencia
{
    public class DataUsuarios
    {
        public static async Task InsertarData(cursosbasesContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    NombreCompleto = "Ernesto Flores ",
                    UserName = "efloresp",
                    Email = "efloresp@veracruz.gob.mx"
                };
                await usuarioManager.CreateAsync(usuario, "Efloresp2013$");
            }
        }
    }
}
