using Microsoft.AspNetCore.Identity;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Propiedad para el nombre completo del usuario (empleado)
        public string FullName { get; set; }

        // Propiedad para el puesto del usuario en la licorería (por ejemplo, "Gerente", "Vendedor", etc.)
        public string Position { get; set; }
    }
}


