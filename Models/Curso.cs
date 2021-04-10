using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cursos.Models
{
    [Table("Curso")]
    public partial class Curso
    {
        public Curso()
        {
            InscripcionCursos = new HashSet<InscripcionCurso>();
        }

        [Key]
        public int IdCurso { get; set; }
        [StringLength(10)]
        public string Codigo { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        [InverseProperty(nameof(InscripcionCurso.IdCursoNavigation))]
        public virtual ICollection<InscripcionCurso> InscripcionCursos { get; set; }
    }
}
