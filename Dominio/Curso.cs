using System;
using System.Collections.Generic;

#nullable disable

namespace Dominio
{
    public partial class Curso
    {
        public Curso()
        {
            Comentarios = new HashSet<Comentario>();
            Cursoinstructors = new HashSet<Cursoinstructor>();
            Precios = new HashSet<Precio>();
        }

        public Guid Idcurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Fotoportada { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Cursoinstructor> Cursoinstructors { get; set; }
        public virtual ICollection<Precio> Precios { get; set; }
    }
}
