
using AutoMapper;
using persistencia;

namespace aplicacion
{
    public class HandlerRequestMapperBase
    {
        public readonly cursosbasesContext _context;
        public readonly IMapper _mapper;
        public HandlerRequestMapperBase(cursosbasesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}