using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Propiedad para el nombre completo del usuario (empleado)
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe superar los 100 caracteres.")]
        public string FullName { get; set; }

        // Propiedad para el puesto del usuario en la licorería
        [Required(ErrorMessage = "El puesto es obligatorio.")]
        [StringLength(50, ErrorMessage = "El puesto no debe superar los 50 caracteres.")]
        public string Position { get; set; }
    }
}

