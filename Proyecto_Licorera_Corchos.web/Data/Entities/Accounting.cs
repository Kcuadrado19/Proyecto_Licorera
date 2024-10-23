using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Relación con la tabla Modifications
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Id_Modification { get; set; } // Clave foránea a Modifications

        [ForeignKey("Id_Modification")]
        public virtual Modifications Modifications { get; set; } // Propiedad de navegación para Modifications

        // Relación con la tabla Sales
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public int Id_User { get; set; } // Clave foránea a Users

        [ForeignKey("Id_User")]
        public virtual Users Users { get; set; } // Propiedad de navegación para User

    }

}
