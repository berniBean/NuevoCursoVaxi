using aplicacion.Cursos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistencia;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: false)
            .Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<cursosbasesContext>(opt => {
    opt.UseMySQL(configuration.GetConnectionString("SefiplanConnection"));
});
builder.Services.AddMediatR(typeof(Consulta.Handler).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
