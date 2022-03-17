using System;
using System.Collections.Generic;

#nullable disable

namespace Dominio
{
    public partial class Cursoinstructor
    {
        public Guid Idcurso { get; set; }
        public Guid Idinstructor { get; set; }

        public virtual Curso IdcursoNavigation { get; set; }
        public virtual Instructor IdinstructorNavigation { get; set; }
    }
}
