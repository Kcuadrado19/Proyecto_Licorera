using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Accounting
    {
        [Key] // Clave primaria
        public int Id_A { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Nombre_A { get; set; }

        [MaxLength(100, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public required string Contrasena_A { get; set; }

        // Relación con la tabla Modificaciones
        public int Id_HModificaciones_A { get; set; }
        //public virtual Modifications Modificaciones { get; set; }


    }

}
