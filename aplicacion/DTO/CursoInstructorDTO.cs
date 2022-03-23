namespace aplicacion.DTO
{
    public class CursoInstructorDTO
    {
        public Guid Idcurso { get; set; }
        public Guid Idinstructor { get; set; }

        public virtual CursoDTO IdcursoNavigation { get; set; }
        public virtual InstructorDTO IdinstructorNavigation { get; set; }
    }
}
