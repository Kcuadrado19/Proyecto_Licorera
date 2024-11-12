using Microsoft.AspNetCore.Identity;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class RolePermission
    {
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}

