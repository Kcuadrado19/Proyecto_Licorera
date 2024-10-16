using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Accounting
    {
        [Key] // Clave primaria
        public int Id_Accounting { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Accounting_Name { get; set; }

        [MaxLength(100, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public required string Password { get; set; }

        // Relación con la tabla Modificaciones
        public int Id_Modification { get; set; }
        public virtual Modifications Modifications { get; set; }

    }

}
