using System.ComponentModel.DataAnnotations; // lady: Importa las anotaciones de datos
using Microsoft.AspNetCore.Identity; // lady: Importa Identity para extender el usuario de identidad

namespace Proyecto_Licorera_Corchos.web.Data.Entities // lady: Espacio de nombres corregido a Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // lady: Propiedad para el nombre completo del usuario (empleado)
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no debe superar los 100 caracteres.")]
        public string FullName { get; set; }

        // lady: Propiedad para el puesto del usuario en la licorería
        [Required(ErrorMessage = "El puesto es obligatorio.")]
        [StringLength(50, ErrorMessage = "El puesto no debe superar los 50 caracteres.")]
        public string Position { get; set; }
    }
}


