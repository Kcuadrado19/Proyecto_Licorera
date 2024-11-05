using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Services;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Helpers;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SalesController : Controller
    {

        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService) 
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            Response<List<Sales>> response= await _salesService.GetlistAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sales sales1) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(sales1);
                }

                Response<Sales> response= await _salesService.CreateAsync(sales1);

                if (response.IsSuccess) 
                { 
                    return RedirectToAction(nameof(Index));
                }

                //TODO:Mostrar mensaje de error
                return View(response);

            }
            catch (Exception ex) 
            {
                return View(sales1);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id_Sales)
        {
            Response<Sales> response = await _salesService.GetOneAsync(Id_Sales);

            if (response.IsSuccess) 
            {
                
                return View(response.Result);
            }
            //TODO:MENSAJE DE ERROR
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Sales sales1)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //TODO:Mostrar mensaje de error
                    return View(sales1);
                }

                Response<Sales> response = await _salesService.EditAsync(sales1);

                if (response.IsSuccess)
                {
                    //TODO:Mostrar mensaje de exito
                    return RedirectToAction(nameof(Index));
                }

                //TODO:Mostrar mensaje de error
                return View(response);
            }
            catch (Exception ex)
            {
                //TODO:Mostrar mensaje de error
                return View(sales1);
            }
        }
    }
}
