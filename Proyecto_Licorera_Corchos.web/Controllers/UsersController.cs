using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    [Authorize(Roles = "Admin")] // Solo los administradores pueden gestionar usuarios
    public class UsersController : Controller
    {

        private readonly IUserService _userService;
        private readonly INotyfService _notifyService;


        public UsersController(IUserService userService, INotyfService notifyService)
        {
            _userService = userService;
            _notifyService = notifyService;
        }




        public async Task<IActionResult> Index(int? RecordsPerPage, int? Page, string? Filter)
        {
            PaginationRequest request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 10,
                Page = Page ?? 1,
                Filter = Filter
            };

            var response = await _userService.GetlistAsync(request);

            if (!response.IsSuccess)
            {
                TempData["ErrorMessage"] = response.Message;
                return View(new List<ApplicationUser>()); // Si hay un error, devolver una lista vacía
            }

            return View(response.Result);
        }




        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, string password)
        {
            ModelState.Remove("Sales"); // Eliminar la validación de la propiedad "Sales"

            if (ModelState.IsValid)
            {
                user.UserName = user.Email; 

                // Crear el usuario utilizando IUserService
                var result = await _userService.CreateUserAsync(user, password);

                if (result)
                {
                    //TempData["SuccessMessage"] = "¡Usuario creado exitosamente! Bienvenido a nuestro equipo de la licorería.";
                    _notifyService.Success("¡Usuario creado exitosamente! Bienvenido a nuestro equipo de la licorería.");
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "No se pudo crear el usuario. Verifica los datos ingresados.");
            }
            else
            {
                // Mostrar los errores de validación del ModelState
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
            }

            //TempData["ErrorMessage"] = "¡Ups! Hubo un problema al crear el usuario. Por favor, verifica los datos.";
            _notifyService.Error("¡Ups! Hubo un problema al crear el usuario. Por favor, verifica los datos.");
            return View(user);
        }




        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser user)
        {
            ModelState.Remove("Sales");
            if (id != user.Id)
            {
                Console.WriteLine("Error: el ID del usuario no coincide.");
                return NotFound();
            }

            Console.WriteLine("Intentando actualizar el usuario con ID: " + user.Id);
            Console.WriteLine("Email: " + user.Email);
            Console.WriteLine("Nombre completo: " + user.FullName);

            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(user);
                if (result)
                {
                    TempData["SuccessMessage"] = "¡Usuario actualizado con éxito!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Console.WriteLine("Error al intentar actualizar el usuario.");
                    ModelState.AddModelError(string.Empty, "Hubo un problema al actualizar el usuario. Verifica los datos ingresados.");
                }
            }
            else
            {
                Console.WriteLine("Modelo inválido.");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error de validación: {error.ErrorMessage}");
                }
            }

            TempData["ErrorMessage"] = "¡Ups! No se pudo actualizar el usuario. Inténtalo de nuevo.";
            return View(user);
        }









        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ModelState.Remove("Sales");
            if (id == null)
            {
                TempData["ErrorMessage"] = "ID de usuario no proporcionado.";
                return RedirectToAction(nameof(Index));
            }

            var result = await _userService.DeleteUserAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "¡Usuario eliminado correctamente!";
            }
            else
            {
                TempData["ErrorMessage"] = "No se pudo eliminar el usuario. Inténtalo más tarde.";
            }

            return RedirectToAction(nameof(Index));
        }


        // Acción para el inicio de sesión (accesible para todos)
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }




        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignInUserAsync(model.Username, model.Password, model.RememberMe);
                if (result)
                {
                    TempData["SuccessMessage"] = "¡Bienvenido de nuevo!";
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
            }
            return View(model);
        }


        // Acción para el registro de usuarios (accesible para todos)
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    FullName = model.Username,
                    Position = "Vendedor", // lady: Define un puesto por defecto para los usuarios registrados
                    Email = model.Username // lady: Se asume que el nombre de usuario es el correo electrónico
                };

                var result = await _userService.RegisterUserAsync(user, model.Password);
                if (result)
                {
                    TempData["SuccessMessage"] = "¡Registro exitoso! Bienvenido a la familia de la licorería.";
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "No se pudo completar el registro. Verifica los datos ingresados.");
            }
            TempData["ErrorMessage"] = "No se pudo completar el registro. Por favor, verifica los datos.";
            return View(model);
        }





        // Acción para cerrar sesión (accesible para todos)
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutUserAsync();
            TempData["SuccessMessage"] = "¡Has cerrado sesión exitosamente!";
            return RedirectToAction("Index", "Home");
        }
    }
}


