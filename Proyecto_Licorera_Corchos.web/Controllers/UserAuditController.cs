using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class UserAuditController : Controller
    {
        private readonly DataContext _context;

        public UserAuditController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
        List<UsersAudit> UsersAudits1 = await _context.UsersAudit.ToListAsync();
        return View(UsersAudits1);
        }
    }
}
