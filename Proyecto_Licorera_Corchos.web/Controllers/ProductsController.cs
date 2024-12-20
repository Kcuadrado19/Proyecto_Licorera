﻿
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;
using System.Threading.Tasks;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Helpers;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
   
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        private readonly INotyfService _notifyService;

        public ProductsController(IProductService productService, INotyfService notifyService)
        {
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
            Response<PaginationResponse<Product>> response = await _productService.GetlistAsync(request);
            return View(response.Result);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var response = await _productService.GetOneAsync(Id.Value);
            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validacion");
                    return View(product);
                }

                Response<Product> response = await _productService.CreateAsync(product);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(product);

            }
            catch (Exception ex)
            {
                _notifyService.Error("Ocurrió un error inesperado al crear el producto.");
                return View(product);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Response<Product> response = await _productService.GetOneAsync(Id);

            if (response.IsSuccess)
            {

                return View(response.Result);
            }
            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validacion");
                    return View(product);
                }

                Response<Product> response = await _productService.EditAsync(product);

                if (response.IsSuccess)
                {
                    _notifyService.Success("Producto editado con éxito");
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(product);
            }
            catch (Exception ex)
            {
                _notifyService.Error("Ocurrió un error al editar el producto.");
                return View(product);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete( int Id)
        {
            Response<Product> response = await _productService.DeleteAsync(Id);

            if (response.IsSuccess)
            {
                _notifyService.Success("El producto ha sido eliminado con éxito");

            }
            else
            {
                _notifyService.Error("Error al intentar eliminar el producto");
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var response = await _productService.DeleteAsync(Id);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error", response.Message);
        }


    }
}
