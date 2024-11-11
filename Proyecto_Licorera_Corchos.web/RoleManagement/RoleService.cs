using Microsoft.AspNetCore.Identity;
using Proyecto_Licorera_Corchos.web.Helpers;
using Proyecto_Licorera_Corchos.web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Proyecto_Licorera_Corchos.web.RoleManagement
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            // lady: Obtenemos todos los roles
            return await Task.FromResult(new List<IdentityRole>(_roleManager.Roles));
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            // lady: Verificamos si el rol ya existe
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return false;
            }

            IdentityRole role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            // lady: Obtenemos el rol por su Id
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<bool> UpdateRoleAsync(string roleId, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            role.Name = newRoleName;
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }
    }
}
