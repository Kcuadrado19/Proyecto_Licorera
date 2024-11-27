using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.DTOs;
using Proyecto_Licorera_Corchos.web.Services;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using AspNetCoreHero.ToastNotification.Abstractions;
using Proyecto_Licorera_Corchos.web.Attributes;



namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolesService _roleService;
        private readonly INotyfService _notifyService;

        public RolesController(IRolesService roleService, INotyfService notifyService)
        {
            _roleService = roleService;
            _notifyService = notifyService;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showRoles", module: "Roles")]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery] int? Page,
                                               [FromQuery] string? Filter)
        {
            PaginationRequest paginationRequest = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter,
            };

            var response = await _roleService.GetListAsync(paginationRequest);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction("Error", "Home");
            }

            return View(response.Result);
        }

        [HttpGet]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create()
        {
            var permissionsResponse = await _roleService.GetPermissionsAsync();
            var sectionsResponse = await _roleService.GetSectionsAsync();

            if (!permissionsResponse.IsSuccess || !sectionsResponse.IsSuccess)
            {
                _notifyService.Error("Error al cargar permisos o secciones.");
                return RedirectToAction(nameof(Index));
            }

            var dto = new RoleDto
            {
                Permissions = permissionsResponse.Result,
                Sections = sectionsResponse.Result
            };

            return View(dto);
        }

        [HttpPost]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create(RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación.");
                return View(dto);
            }

            var createResponse = await _roleService.CreateAsync(dto);

            if (createResponse.IsSuccess)
            {
                _notifyService.Success(createResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(createResponse.Message);
            return View(dto);
        }

        [HttpGet]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _roleService.GetOneAsync(id);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

      
        [HttpPost]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación.");
                return View(dto);
            }

            var editResponse = await _roleService.EditAsync(dto);

            if (editResponse.IsSuccess)
            {
                _notifyService.Success(editResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(editResponse.Message);
            return View(dto);
        }

    }
}
