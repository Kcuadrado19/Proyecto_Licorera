using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class PermisosController : Controller
    {
        private readonly DataContext _context;

        public PermisosController(DataContext context)
        {
            _context = context;
        }
    }
}
