using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Requests;
using Proyecto_Licorera_Corchos.web.Services;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ISectionsService _sectionsService;
        private readonly INotyfService _notifyService;

        public SectionsController(ISectionsService sectionsService, INotyfService notifyService)
        {
            _sectionsService = sectionsService;
            _notifyService = notifyService;
        }

        // GET: /Sections
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery] int? Page,
                                               [FromQuery] string? Filter)
        {
            var request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter
            };

            var response = await _sectionsService.GetListAsync(request);
            return View(response.Result);
        }

        // GET: /Sections/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Sections/Create
        [HttpPost]
        public async Task<IActionResult> Create(Section section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    return View(section);
                }

                var response = await _sectionsService.CreateAsync(section);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(section); // Regresa el modelo en caso de error
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return View(section);
            }
        }

        // GET: /Sections/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var response = await _sectionsService.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Sections/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Section section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    return View(section);
                }

                var response = await _sectionsService.EditAsync(section);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(section); // Regresa el modelo en caso de error
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return View(section);
            }
        }

        // POST: /Sections/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _sectionsService.DeleteteAsync(id);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
            }
            else
            {
                _notifyService.Error(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Sections/Toggle
        [HttpPost]
        public async Task<IActionResult> Toggle(int SectionId, bool Hide)
        {
            var request = new ToggleSectionStatusRequest
            {
                Hide = Hide,
                SectionId = SectionId
            };

            var response = await _sectionsService.ToggleAsync(request);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
            }
            else
            {
                _notifyService.Error(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
