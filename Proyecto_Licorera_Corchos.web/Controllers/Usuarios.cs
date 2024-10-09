using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }
    }
}
