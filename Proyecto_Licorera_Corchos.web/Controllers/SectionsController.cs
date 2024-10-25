using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;
using Microsoft.EntityFrameworkCore;


namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SectionsController : Controller
    {

        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        public async Task <IActionResult> Index()
        {
            return View(await _sectionService.GetlistAsync());
        }
    }
}
