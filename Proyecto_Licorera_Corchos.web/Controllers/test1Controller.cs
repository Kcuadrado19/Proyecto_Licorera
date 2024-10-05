using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class test1Controller : Controller
    {
        private readonly DataContext _context;

        public test1Controller(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<test1> tests1 = await _context.test1.ToListAsync();
            return View(tests1);
        }
    }
}
