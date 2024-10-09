using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class ModificacionesController : Controller
    {
        private readonly DataContext _context;

        public ModificacionesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Modificaciones> Modificaciones1 = await _context.Modificaciones.ToListAsync();
            return View(Modificaciones1);
        }
    }
}
