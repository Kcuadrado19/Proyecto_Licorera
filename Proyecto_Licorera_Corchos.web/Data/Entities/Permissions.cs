using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Permissions
    {
        [Key] // Clave primaria
        public int Id_Rol { get; set; }

        [MaxLength(50, ErrorMessage = "El campo '{0}' debe tener máximo '{1}' caracteres")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Permiso { get; set; }
    }

}
