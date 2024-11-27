using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Para ICollection

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Permiso")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [MaxLength(512, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Módulo")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Module { get; set; } = string.Empty;

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}



