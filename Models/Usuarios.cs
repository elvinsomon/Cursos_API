using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cursos.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario {get; set;}

        [Required(ErrorMessage = "El usuario no puede estar vacio.")]
        public string Usuario {get; set;}
        
        [Required(ErrorMessage = "La clave no puede estar vacio.")]
        public string Clave {get; set;}

        [Compare("Clave", ErrorMessage = "Las contraseñas no coninciden.")]
        [NotMapped]
        public string ConfirmarClave {get; set;}
        public string Sal {get; set;}
    }
}