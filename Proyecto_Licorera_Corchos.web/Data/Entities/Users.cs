using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Users
    {
        [Key] // Clave primaria

        public int Id_Users { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string? Rol { get; set; }

        [MaxLength(100, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string? Contrasenna { get; set; }

        // Relación con la tabla Permisos
        public int Id_Rol { get; set; }
        public virtual Permissions? Permisos { get; set; }
    }

}
