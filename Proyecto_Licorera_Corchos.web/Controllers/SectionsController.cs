using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Proyecto_Licorera_Corchos.web.Core;
using System.Collections.Generic;
using Proyecto_Licorera_Corchos.web.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;


namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly INotyfService _notifyService;

        public SectionsController(ISectionService sectionService, INotyfService notifyService)
        {
            _sectionService = sectionService;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _notifyService.Success("This is a success notification");
            Response<List<Section>> response = await _sectionService.GetlistAsync();
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Section> response = await _sectionService.GetOneAsync(id);
            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }

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

                Response<Section> response = await _sectionService.EditAsync(section);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(section);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return View(section);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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

                Response<Section> response = await _sectionService.CreateAsync(section);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(section);
            }
            catch (Exception ex)
            {
                _notifyService.Error("Error al generar la solicitud: " + ex.Message);
                Console.WriteLine(ex); // Registro detallado de la excepción en la consola
                return View(section);
            }
        }
    }
}





