using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var roles = await _roleService.GetAllRolesAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling(roles.Count / 10.0); // lady: Paginación simple
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Permissions = await _roleService.GetPermissionsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName, List<string> permissions)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "El nombre del rol es requerido.");
                return View();
            }

            var result = await _roleService.CreateRoleAsync(roleName);
            if (result)
            {
                // lady: Aquí puedes guardar los permisos asignados si tienes una estructura para eso
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error al crear el rol.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            ViewBag.Permissions = await _roleService.GetPermissionsAsync();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string roleId, string newRoleName, List<string> selectedPermissions)
        {
            if (string.IsNullOrEmpty(newRoleName))
            {
                ModelState.AddModelError("", "El nuevo nombre del rol es requerido.");
                return View();
            }

            var result = await _roleService.UpdateRoleAsync(roleId, newRoleName);
            if (result)
            {
                // lady: Aquí puedes actualizar los permisos asignados si es necesario
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error al actualizar el rol.");
            return View();
        }

        public async Task<IActionResult> Delete(string roleId)
        {
            var result = await _roleService.DeleteRoleAsync(roleId);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error al eliminar el rol.");
            return RedirectToAction("Index");
        }
    }
}

