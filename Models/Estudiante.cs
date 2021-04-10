using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Cursos.Models
{
    [Table("Estudiante")]
    public partial class Estudiante
    {
        public Estudiante()
        {
            Matriculas = new HashSet<Matricula>();
        }

        [Key]
        public int IdEstudiante { get; set; }
        [StringLength(10)]
        public string Codigo { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(101)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string NombreApellido { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        [InverseProperty(nameof(Matricula.IdEstudianteNavigation))]
        public virtual ICollection<Matricula> Matriculas { get; set; }
    }
}
