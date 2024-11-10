using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Services;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;
using Proyecto_Licorera_Corchos.web.Core.Pagination;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SalesController : Controller
    {

        private readonly ISalesService _salesService;

        private readonly INotyfService _notifyService;

        public SalesController(ISalesService salesService, INotyfService notifyService)
        {
            _salesService = salesService;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task <IActionResult> Index([FromQuery] int? RecordsPerPage,
                                                [FromQuery] int? Page,
                                                [FromQuery] string? Filter)
        {
           
            PaginationRequest request = new PaginationRequest 
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter=Filter
            };
            Response<PaginationResponse<Sales>> response= await _salesService.GetlistAsync(request);
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
                    _notifyService.Error("Debe ajustar los errores de validacion");
                    return View(sales1);
                }

                Response<Sales> response= await _salesService.CreateAsync(sales1);

                if (response.IsSuccess) 
                { 
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
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
            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Sales sales1)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validacion");
                    return View(sales1);
                }

                Response<Sales> response = await _salesService.EditAsync(sales1);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(response);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return View(sales1);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int Id_Sales)
        {
            Response<Sales> response = await _salesService.DeleteAsync(Id_Sales);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
               
            }
            else
            {
                _notifyService.Error(response.Message);
            }
            
            return RedirectToAction(nameof(Index));

        }
    }
}
