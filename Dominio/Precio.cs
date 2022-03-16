using System;
using System.Collections.Generic;

#nullable disable

namespace Dominio
{
    public partial class Precio
    {
        public int PrecioId { get; set; }
        public decimal PrecioActual { get; set; }
        public decimal Promocion { get; set; }
        public int CursoId { get; set; }

        public virtual Curso Curso { get; set; }
    }
}
