
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;
using System.Threading.Tasks;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllAsync();
            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            return View("Error", response.Message); // Manejo de errores
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _productService.GetByIdAsync(id.Value);
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
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateAsync(product);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                // Si hubo error, mostramos un mensaje
                return View("Error", response.Message);
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _productService.GetByIdAsync(id.Value);
            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _productService.EditAsync(product);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View("Error", response.Message);
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _productService.GetByIdAsync(id.Value);
            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _productService.DeleteAsync(id);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error", response.Message);
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

        
    }
}
