using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class AccountingController : Controller
    {
        private readonly DataContext _context;

        public AccountingController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Accounting> Accounting1 = await _context.Accounting.ToListAsync();
            return View(Accounting1);
        }
    }
}
