using Microsoft.AspNetCore.Authorization; // lady: Importamos la librería para la autorización
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
    [Authorize(Roles = "Vendedor,Admin")] // lady: Solo los roles de Vendedor y Admin pueden acceder a este controlador
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService; // lady: Inyectamos el servicio de ventas
        private readonly INotyfService _notifyService; // lady: Inyectamos el servicio de notificaciones

        public SalesController(ISalesService salesService, INotyfService notifyService)
        {
            _salesService = salesService; // lady: Asignamos el servicio de ventas
            _notifyService = notifyService; // lady: Asignamos el servicio de notificaciones
        }

        [HttpGet]

        public async Task <IActionResult> Index([FromQuery] int? RecordsPerPage,
                                                [FromQuery] int? Page,
                                                [FromQuery] string? Filter)
        {
            // lady: Creamos una solicitud de paginación


            // lady: Obtenemos la lista paginada de ventas
            Response<PaginationResponse<Sales>> response = await _salesService.GetlistAsync(request);
            return View(response.Result); // lady: Devolvemos la vista con la lista de ventas
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); // lady: Devolvemos la vista para crear una venta
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // lady: Protección contra ataques CSRF
        public async Task<IActionResult> Create(Sales sales1)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación"); // lady: Notificación de error de validación
                    return View(sales1); // lady: Devolvemos la vista con los datos ingresados
                }

                // lady: Intentamos crear la venta
                Response<Sales> response = await _salesService.CreateAsync(sales1);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message); // lady: Notificación de éxito
                    return RedirectToAction(nameof(Index)); // lady: Redirigimos a la lista de ventas
                }

                _notifyService.Error(response.Message); // lady: Notificación de error
                return View(response);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message); // lady: Manejamos cualquier excepción
                return View(sales1);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int Id_Sales)
        {
            // lady: Obtenemos la venta a editar
            Response<Sales> response = await _salesService.GetOneAsync(Id_Sales);

            if (response.IsSuccess)
            {
                return View(response.Result); // lady: Devolvemos la vista con la venta encontrada
            }

            _notifyService.Error(response.Message); // lady: Notificación de error si no se encuentra la venta
            return RedirectToAction(nameof(Index)); // lady: Redirigimos a la lista de ventas
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // lady: Protección contra ataques CSRF
        public async Task<IActionResult> Edit(Sales sales1)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación"); // lady: Notificación de error de validación
                    return View(sales1);
                }

                // lady: Intentamos actualizar la venta
                Response<Sales> response = await _salesService.EditAsync(sales1);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message); // lady: Notificación de éxito
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message); // lady: Notificación de error
                return View(response);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message); // lady: Manejamos cualquier excepción
                return View(sales1);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // lady: Protección contra ataques CSRF
        public async Task<IActionResult> Delete([FromRoute] int Id_Sales)
        {
            // lady: Intentamos eliminar la venta
            Response<Sales> response = await _salesService.DeleteAsync(Id_Sales);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message); // lady: Notificación de éxito
            }
            else
            {
                _notifyService.Error(response.Message); // lady: Notificación de error
            }

            return RedirectToAction(nameof(Index)); // lady: Redirigimos a la lista de ventas
        }
    }
}
