using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cursos.Models
{
    [Table("InscripcionCurso")]
    public partial class InscripcionCurso
    {
        [Key]
        public int IdEstudiante { get; set; }
        [Key]
        public int IdPeriodo { get; set; }
        [Key]
        public int IdCurso { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fecha { get; set; }

        [ForeignKey("IdEstudiante,IdPeriodo")]
        [InverseProperty(nameof(Matricula.InscripcionCursos))]
        public virtual Matricula Id { get; set; }
        [ForeignKey(nameof(IdCurso))]
        [InverseProperty(nameof(Curso.InscripcionCursos))]
        public virtual Curso IdCursoNavigation { get; set; }
    }
}
