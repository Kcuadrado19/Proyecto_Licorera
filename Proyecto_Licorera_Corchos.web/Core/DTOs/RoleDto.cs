using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.DTOs
{
    public class RoleDTO
    {
        public string Id { get; set; } // Identificador del rol (string para compatibilidad con Identity)

        [Display(Name = "Rol")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        public List<PermissionDTO>? Permissions { get; set; } // Lista de permisos relacionados

        public string? PermissionIds { get; set; } // IDs de los permisos en formato JSON

        public List<SectionDTO>? Sections { get; set; } // Lista de secciones relacionadas

        public string? SectionIds { get; set; } // IDs de las secciones en formato JSON
    }
}