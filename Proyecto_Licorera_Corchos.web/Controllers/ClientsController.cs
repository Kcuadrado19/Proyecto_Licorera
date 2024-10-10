using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class ClientsController:Controller
    {
        private readonly DataContext _context;

        public ClientsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Clients> clientes1 = await _context.Clientes.ToListAsync();
            return View(clientes1);
        }

    }
}
