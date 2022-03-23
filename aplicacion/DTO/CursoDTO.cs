namespace aplicacion.DTO
{
    public class CursoDTO
    {

        public Guid Idcurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Fotoportada { get; set; }

        public ICollection<InstructorDTO> instructors { get; set; }
    }
}
