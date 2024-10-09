using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class AUsuariosController : Controller
    {
        private readonly DataContext _context;

        public AUsuariosController(DataContext context)
        {
            _context = context;
        }
    }
}
