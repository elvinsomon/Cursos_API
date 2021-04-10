using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cursos.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            Matricula = new HashSet<Matricula>();
        }

        [Key]
        public int IdEstudiante { get; set; }

        [StringLength(10)]
        public string Codigo { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string NombreApellido { get; set; }


        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        public virtual ICollection<Matricula> Matricula { get; set; }
    }
}
