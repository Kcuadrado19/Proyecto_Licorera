using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<bool> UpdateRoleAsync(string roleId, string newRoleName);
        Task<IdentityRole> GetRoleByIdAsync(string roleId);

        // lady: Método para obtener los permisos
        Task<List<string>> GetPermissionsAsync();
    }
}

