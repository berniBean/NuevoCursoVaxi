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
                .ForMember(x => x.instructors, y => y.MapFrom(z => z.Cursoinstructors.Select(x => x.IdinstructorNavigation).ToList()));
            CreateMap<Cursoinstructor, CursoInstructorDTO>();
            CreateMap<Dominio.Instructor, InstructorDTO>();



        }
    }
}
