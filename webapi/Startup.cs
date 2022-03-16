using aplicacion.Cursos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using persistencia;


namespace webapi
{
    public  class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        public  void ConfigureServices(IServiceCollection Services)
        {
            // Add services to the container.
            Services.AddDbContext<cursosbasesContext>(opt => {
                opt.UseMySQL(Configuration.GetConnectionString("SefiplanConnection"));
                });
            Services.AddMediatR(typeof(Consulta.Handler).Assembly);
            Services.AddControllers();
           
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            

        }

        public  void Configure(IApplicationBuilder app)
        {

        }
        
    }
}