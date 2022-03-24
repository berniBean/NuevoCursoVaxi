using aplicacion.DTO;
using AutoMapper;
using Dominio;

namespace aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoDTO>()
                .ForMember(x => x.instructors, y => y.MapFrom(z => z.Cursoinstructors
                .Select(x => x.IdinstructorNavigation).ToList()))
                .ForMember(x => x.comentarios, y => y.MapFrom(z => z.Comentarios))
                .ForMember(x => x.precio, y => y.MapFrom(z => z.Precios));


            CreateMap<Cursoinstructor, CursoInstructorDTO>();
            CreateMap<Dominio.Instructor, InstructorDTO>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<Precio, PrecioDTO>();



        }
    }
}
