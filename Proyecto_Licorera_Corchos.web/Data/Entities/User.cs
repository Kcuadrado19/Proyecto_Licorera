using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static Proyecto_Licorera_Corchos.web.Data.Entities.IdentityUserToken;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe terner máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public int PrivateBlogRoleId { get; set; }

        public LicoreraRole PrivateBlogRole { get; set; }
    }
}
