using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;


namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SectionsController : Controller
    {
        public IActionResult Index()
        {
            List<Section> sections = new List<Section>

            {
                new Section
                {
                    Id = 1,
                    Name = "Base",
                    Description = "Test 1"
                },

                new Section
                {
                    Id = 1,
                    Name = "Hacking",
                    Description = "Test 2"
                },

                new Section
                {
                    Id = 1,
                    Name = "Telecomunicaciones",
                    Description = "Test 3"
                },


            };

            return View(sections);
        }
    }
}
