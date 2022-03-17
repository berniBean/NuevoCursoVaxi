using System;
using System.Collections.Generic;

#nullable disable

namespace Dominio
{
    public partial class Instructor
    {
        public Instructor()
        {
            Cursoinstructors = new HashSet<Cursoinstructor>();
        }

        public Guid Idinstructor { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Grado { get; set; }
        public string Foto { get; set; }

        public virtual ICollection<Cursoinstructor> Cursoinstructors { get; set; }
    }
}
