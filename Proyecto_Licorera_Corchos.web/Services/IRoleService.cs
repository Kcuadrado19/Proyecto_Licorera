using Microsoft.AspNetCore.Identity;
using Proyecto_Licorera_Corchos.web.Data.Entities;
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
    }
}

