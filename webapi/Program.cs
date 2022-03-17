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

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: false)
            .Build();


// Add services to the container.

builder.Services.AddControllers(
        
    ).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

var build = builder.Services.AddIdentityCore<Usuario>();
var identityBuilder = new IdentityBuilder(build.UserType, builder.Services);
identityBuilder.AddEntityFrameworkStores<cursosbasesContext>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();





builder.Services.AddDbContext<cursosbasesContext>(opt => {
    opt.UseMySQL(configuration.GetConnectionString("SefiplanConnection"));
});
builder.Services.AddMediatR(typeof(Consulta.Handler).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title =" Servicios de mantenimiento de cursos",
        Version ="v1"
    });
    c.CustomSchemaIds(c => c.FullName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ManejadorErrorMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cursos online v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
