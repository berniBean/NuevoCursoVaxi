using aplicacion.ManejadorError;
using Newtonsoft.Json;
using System.Net;

namespace webapi.Middleware
{
    public class ManejadorErrorMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger _logger;

        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await ManejadorExcepcionAsync(context, ex, _logger);
            }

        }

        private async Task ManejadorExcepcionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            object errores = null;
            switch (ex)
            {
                case ManejadorExcepcion me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me._Errores;
                    context.Response.StatusCode = (int)me._Codigo;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

                default:
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}
