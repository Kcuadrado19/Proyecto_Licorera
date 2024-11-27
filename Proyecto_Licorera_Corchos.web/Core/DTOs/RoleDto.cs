using System.ComponentModel.DataAnnotations;

namespace Proyecto_Licorera_Corchos.web.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; } // Cambiar el tipo a int si la base de datos usa int como Id

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [MaxLength(64, ErrorMessage = "El nombre no puede exceder los 64 caracteres.")]
        public string Name { get; set; }

        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
        public string PermissionIds { get; set; } = string.Empty;

        public List<SectionDto> Sections { get; set; } = new List<SectionDto>();
        public string SectionIds { get; set; } = string.Empty;
    }
}


