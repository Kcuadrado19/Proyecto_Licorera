using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Orders> Orders1 = await _context.Orders.ToListAsync();
            return View(Orders1);
        }

        //[HttpGet]

        //public IActionResult Create()
        //{
        //   return View();
       // }
    }
}
