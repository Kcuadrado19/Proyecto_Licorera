namespace Proyecto_Licorera_Corchos.web.DTOs
{
    public class RoleDto
    {
        public string Id { get; set; } // El ID del rol
        public string Name { get; set; } // El nombre del rol
        public List<string> AssignedPermissions { get; set; } = new List<string>(); // Lista de permisos asignados
    }
}
