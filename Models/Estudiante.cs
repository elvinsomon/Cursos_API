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
        [Required(ErrorMessage = "El codigo es requerido.")]
        [MinLength(10, ErrorMessage = "El codigo debe tener un minimo de 10 caractenes.")]
        [MaxLength(10, ErrorMessage = "El codigo debe tener un maximo de 10 caractenes.")]
        public string Codigo { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MinLength(3, ErrorMessage = "El nombre debe tener un minimo de 3 caractenes.")]
        [MaxLength(50, ErrorMessage = "El nombre debe tener un maximo de 50 caractenes.")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El apellido es requerido.")]
        [MinLength(3, ErrorMessage = "El apellido debe tener un minimo de 3 caractenes.")]
        [MaxLength(50, ErrorMessage = "El apellido debe tener un maximo de 50 caractenes.")]
        public string Apellido { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string NombreApellido { get; set; }


        [Column(TypeName = "date")]
        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [DataType(DataType.Date, ErrorMessage = "El formato de la fecha es invalido")]
        public DateTime? FechaNacimiento { get; set; }

        public virtual ICollection<Matricula> Matricula { get; set; }
    }
}
