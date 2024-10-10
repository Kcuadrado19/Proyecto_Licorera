using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class ModificationsController : Controller
    {
        private readonly DataContext _context;

        public ModificationsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

        List<Modifications> Modifications1 = await _context.Modifications.ToListAsync();
        return View(Modifications1);
        }
    }
}
