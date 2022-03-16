
using persistencia;

namespace aplicacion
{
    public class HandlerRequestBase
    {
        public readonly cursosbasesContext _context;
        public HandlerRequestBase(cursosbasesContext context)
        {
            _context = context;
        }
    }
}