using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe superar los 100 caracteres.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "El puesto es obligatorio.")]
        [StringLength(50, ErrorMessage = "El puesto no debe superar los 50 caracteres.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Introduce un correo electrónico válido.")]
        public override string Email { get; set; }

        // Relación con las ventas realizadas por el usuario
        public ICollection<Sales> Sales { get; set; }
    }
}


