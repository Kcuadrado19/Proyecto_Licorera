using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Core;


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
        public async Task <IActionResult> Index()
        {
            Response<List<Section>> response = await _sectionService.GetlistAsync();
            return View(response.Result);
        }
    }
}
