using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacion.DTO
{
    public class PrecioDTO
    {
        public Guid PrecioId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioActual { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Promocion { get; set; }
        public Guid CursoId { get; set; }

        public virtual CursoDTO Curso { get; set; }
    }
}
