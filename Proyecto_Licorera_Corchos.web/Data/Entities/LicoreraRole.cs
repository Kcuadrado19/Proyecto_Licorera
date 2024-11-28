using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class LicoreraRole
    {
        
            [Key]
            public int Id { get; set; }

            [Display(Name = "Rol")]
            [MaxLength(64, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
            [Required(ErrorMessage = "El campo {0} es requerido.")]
            public string Name { get; set; } = null!;

            public ICollection<RolePermission>? RolePermissions { get; set; }
            public ICollection<RoleSection>? RoleSections { get; set; }
        
    }
}

