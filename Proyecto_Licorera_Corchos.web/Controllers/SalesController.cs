using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Services;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class SalesController : Controller
    {

        private readonly ISalesService _salesService;

        private readonly INotyfService _notifyService;

        private readonly IProductService _productService;

        public SalesController(ISalesService salesService, INotyfService notifyService, IProductService productService)
        {
            _salesService = salesService;
            _productService = productService;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                                [FromQuery] int? Page,
                                                [FromQuery] string? Filter)
        {

            PaginationRequest request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter
            };
            Response<PaginationResponse<Sale>> response = await _salesService.GetlistAsync(request);
            return View(response.Result);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Id", "Name");
            //return View();
            return View(new Sale());
        }

        [HttpPost]


        [HttpPost]
        public async Task<IActionResult> Create(Sale sales1)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");
            ModelState.Remove("Product");
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Id", "Name");
                    _notifyService.Error("Debe ajustar los errores de validación");
                    return View(sales1);
                }

                Response<Sale> response = await _salesService.CreateAsync(sales1);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Id", "Name");
                _notifyService.Error(response.Message);
                return View(sales1);

            }


            catch (Exception ex)
            {
                return View(sales1);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id_Sales)
        {
            try
            {
                var response = await _salesService.GetOneAsync(Id_Sales);

                if (response.IsSuccess && response.Result != null)
                {
                    var products = await _productService.GetAllAsync();
                    if (products == null || !products.Any())
                    {
                        _notifyService.Error("No hay productos disponibles. Agregue al menos un producto antes de editar una venta.");
                        return RedirectToAction(nameof(Index));
                    }

                    // Crear una lista explícita de SelectListItem
                    ViewBag.Products = products
                        .Select(p => new SelectListItem
                        {
                            Value = p.Id.ToString(),
                            Text = p.Name
                        })
                        .ToList();

                    return View(response.Result);
                }

                _notifyService.Error("La venta no fue encontrada.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Ocurrió un error al cargar los datos: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Sale sales1)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");
            ModelState.Remove("Product");
            try
            {
                if (!ModelState.IsValid)
                {
                    // Recargar la lista de productos para el dropdown
                    ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Id", "Name", sales1.ProductId);

                    _notifyService.Error("Debe ajustar los errores de validación");
                    return View(sales1);
                }

                Response<Sale> response = await _salesService.EditAsync(sales1);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Id", "Name", sales1.ProductId);
                //return View(response);
                    return View(sales1);
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error inesperado: {ex.Message}");
                // Recargar la lista de productos para el dropdown en caso de excepción
        ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Id", "Name", sales1.ProductId);
                return View(sales1);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int Id_Sales)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            Response<Sale> response = await _salesService.DeleteAsync(Id_Sales);

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