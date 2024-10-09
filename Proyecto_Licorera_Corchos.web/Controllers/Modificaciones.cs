using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class ModificacionesController : Controller
    {
        private readonly DataContext _context;

        public ModificacionesController(DataContext context)
        {
            _context = context;
        }
    }
}
