using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class PermisosController : Controller
    {
        private readonly DataContext _context;

        public PermisosController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Permisos> Permisos1 = await _context.Permisos.ToListAsync();
            return View(Permisos1);
        }
    }
}
