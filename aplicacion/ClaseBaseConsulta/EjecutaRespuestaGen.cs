using MediatR;

namespace aplicacion
{
    public class EjecutaRespuestaGen<TEntity> : IRequest<TEntity> where TEntity : class
    {
    }
}