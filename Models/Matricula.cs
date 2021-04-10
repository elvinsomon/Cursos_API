using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cursos.Models
{
    [Table("Matricula")]
    public partial class Matricula
    {
        public Matricula()
        {
            InscripcionCursos = new HashSet<InscripcionCurso>();
        }

        [Key]
        public int IdEstudiante { get; set; }
        [Key]
        public int IdPeriodo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Fecha { get; set; }

        [ForeignKey(nameof(IdEstudiante))]
        [InverseProperty(nameof(Estudiante.Matriculas))]
        public virtual Estudiante IdEstudianteNavigation { get; set; }
        [ForeignKey(nameof(IdPeriodo))]
        [InverseProperty(nameof(Periodo.Matriculas))]
        public virtual Periodo IdPeriodoNavigation { get; set; }
        [InverseProperty(nameof(InscripcionCurso.Id))]
        public virtual ICollection<InscripcionCurso> InscripcionCursos { get; set; }
    }
}
