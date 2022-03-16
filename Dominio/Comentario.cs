using System;
using System.Collections.Generic;

#nullable disable

namespace Dominio
{
    public partial class Comentario
    {
        public int Idcomentario { get; set; }
        public string AlumnoName { get; set; }
        public int? Puntaje { get; set; }
        public string Comentario1 { get; set; }
        public int? CursoId { get; set; }

        public virtual Curso Curso { get; set; }
    }
}
