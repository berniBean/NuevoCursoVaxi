using aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using persistencia;
using webapi.Middleware;

namespace webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostServer = CreateHostBuilder(args).Build();

            using (var ambiente = hostServer.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;
                try
                {
                    //para creaci?n de usuarios de prueba
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();
                    var context = services.GetRequiredService<cursosbasesContext>();
                    context.Database.Migrate();
                    DataUsuarios.InsertarData(context, userManager).Wait();
                }
                catch (Exception ex)
                {

                    var loggin = services.GetRequiredService<ILogger<Program>>();
                    loggin.LogError(ex, "Ocurrio un error en la migracion");
                }

            }

            hostServer.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}