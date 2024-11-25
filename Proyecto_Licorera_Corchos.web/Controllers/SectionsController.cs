using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SectionsController : Controller
    {

        //private readonly ISectionsService _sectionsService;
        private readonly INotyfService _notifyService;


        //public SectionsController(ISectionsService sectionsService, INotyfService notifyService)
        //{
        //    _sectionsService = sectionsService;
        //    _notifyService = notifyService;
        //}


        public IActionResult Index()
        {
            return View();
        }
    }
}
