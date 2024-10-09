using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class AUsuariosController : Controller
    {
        private readonly DataContext _context;

        public AUsuariosController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Ausuarios> Ausuarios1 = await _context.Ausuarios.ToListAsync();
            return View(Ausuarios1);
        }
    }
}
