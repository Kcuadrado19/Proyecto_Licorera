using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly DataContext _context;

        public PermissionsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

        List<Permissions> Permissions1 = await _context.Permissions.ToListAsync();
        return View(Permissions1);
        }
    }
}
