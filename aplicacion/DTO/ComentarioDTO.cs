

namespace aplicacion.DTO
{
    public class ComentarioDTO
    {
        public Guid Idcomentario { get; set; }
        public string AlumnoName { get; set; }
        public int? Puntaje { get; set; }
        public string Comentario1 { get; set; }
        public Guid CursoId { get; set; }

    }
}
