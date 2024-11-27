using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static Proyecto_Licorera_Corchos.web.Data.Entities.IdentityUserToken;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe superar los 100 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "El puesto es obligatorio.")]
        [StringLength(50, ErrorMessage = "El puesto no debe superar los 50 caracteres.")]
        [Display(Name = "Rol (Admin o Vendedor)")]
        public string Position { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Introduce un correo electrónico válido.")]
        public override string Email { get; set; }

        // Nueva propiedad RoleId agregada para solucionar el error
        [Display(Name = "ID del Rol")]
        public int? RoleId { get; set; }

        // Relación con LicoreraRole
        public int? LicoreraRoleId { get; set; }
        public LicoreraRole? LicoreraRole { get; set; }

        // Relación con las ventas realizadas por el usuario
        public ICollection<Sales> Sales { get; set; }

        // Claves foráneas para RolePermission
        public int? RolePermissionRoleId { get; set; }
        public int? RolePermissionPermissionId { get; set; }
        public RolePermission? RolePermission { get; set; } // Relación con RolePermission

        // Relación con ApplicationUserRole y ApplicationUserToken
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationUserToken> UserTokens { get; set; }
    }
}
