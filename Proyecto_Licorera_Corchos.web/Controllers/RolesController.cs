using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        // lady: Inyectamos el servicio IRoleService en el controlador
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // lady: Acción para listar todos los roles
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }

        // lady: Acción para mostrar el formulario de creación de roles
        public IActionResult Create()
        {
            return View();
        }

        // lady: Acción para procesar la creación de un nuevo rol
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "El nombre del rol es requerido.");
                return View();
            }

            var result = await _roleService.CreateRoleAsync(roleName);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error al crear el rol.");
                return View();
            }
        }

        // lady: Acción para mostrar el formulario de edición de roles
        public async Task<IActionResult> Edit(string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // lady: Acción para procesar la edición de un rol
        [HttpPost]
        public async Task<IActionResult> Edit(string roleId, string newRoleName)
        {
            if (string.IsNullOrEmpty(newRoleName))
            {
                ModelState.AddModelError("", "El nuevo nombre del rol es requerido.");
                return View();
            }

            var result = await _roleService.UpdateRoleAsync(roleId, newRoleName);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error al actualizar el rol.");
                return View();
            }
        }

        // lady: Acción para eliminar un rol
        public async Task<IActionResult> Delete(string roleId)
        {
            var result = await _roleService.DeleteRoleAsync(roleId);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error al eliminar el rol.");
                return RedirectToAction("Index");
            }
        }
    }
}
