﻿using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SalesController : Controller
    {
        private readonly DataContext _context;

        public SalesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Sales> Sales1 = await _context.Sales.ToListAsync();
            return View(Sales1);
        }
    }
}
