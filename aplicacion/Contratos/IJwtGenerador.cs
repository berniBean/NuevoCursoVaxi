using Dominio;

namespace aplicacion.Contratos
{
    public interface IJwtGenerador
    {
        string CrearToken(Usuario usuario, List<string> roles);
    }
}
