using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class UsersAudit
    {
        [Key] // Clave primaria
        public int Id_AU { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Nombre_AU { get; set; }

        // Relación con la tabla Accounting
        public int Id_A { get; set; }
        
        public virtual Accounting Accounting { get; set; }
    }

}

