using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Proyecto_Licorera_Corchos.web.Core;
using System.Collections.Generic;
using Proyecto_Licorera_Corchos.web.Helpers;




namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SectionsController : Controller
    {

        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Section>> response = await _sectionService.GetlistAsync();
            return View(response.Result);

        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Section section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //TODO: mensaje error
                    return View(section);
                }

                Response<Section> response = await _sectionService.EditAsync(section);

                if (response.IsSuccess)
                {
                    //TODO: mensaje de exito
                    return RedirectToAction(nameof(Index));
                }

                //TODO: Mostrar mensaje de error

                return View(response);
            }

            catch (Exception ex)
            {
                //TODO: mensaje de error
                return View(section);
            }
      
        
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Section> response = await _sectionService.GetOneAsync(id);
            if (response.IsSuccess) 
            {
                return View(response.Result);
            }

            //TODO: mensaje de error
            return RedirectToAction(nameof(Index));
        }


}   }



