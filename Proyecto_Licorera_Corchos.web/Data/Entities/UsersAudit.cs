using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class UsersAudit
    {
        [Key] // Clave primaria
        public int Id_UserAudit { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string UserAudit_Name { get; set; }

        // Relación con la tabla Accounting
        public int Id_Accounting { get; set; }
        
        public virtual Accounting Accounting { get; set; }
    }

}

