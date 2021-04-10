using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cursos.Models
{
    [Table("Periodo")]
    public partial class Periodo
    {
        public Periodo()
        {
            Matriculas = new HashSet<Matricula>();
        }

        [Key]
        public int IdPeriodo { get; set; }
        public int? Anio { get; set; }
        public bool? Estado { get; set; }

        [InverseProperty(nameof(Matricula.IdPeriodoNavigation))]
        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}
