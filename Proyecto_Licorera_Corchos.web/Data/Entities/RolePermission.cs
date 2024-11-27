﻿using Proyecto_Licorera_Corchos.web.Data.Entities;

public class RolePermission
{
    public string RoleId { get; set; }
    public LicoreraRole Role { get; set; }

    public string PermissionId { get; set; }
    public Permission Permission { get; set; }

    // Agregar la colección de usuarios relacionada
    public ICollection<ApplicationUser> Users { get; set; }
}

