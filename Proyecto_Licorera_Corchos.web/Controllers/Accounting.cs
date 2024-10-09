using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class AccountingController : Controller
    {
        private readonly DataContext _context;

        public AccountingController(DataContext context)
        {
            _context = context;
        }
    }
}
