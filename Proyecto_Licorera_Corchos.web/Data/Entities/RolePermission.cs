namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public LicoreraRole Role { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}

