﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // GET: Users/Details/5
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
                user.UserName = user.Email; // Establecer el nombre de usuario al correo electrónico

                // Crear el usuario utilizando IUserService
                var result = await _userService.CreateUserAsync(user, password);
                if (result)
                {
                    TempData["SuccessMessage"] = "¡Usuario creado exitosamente! Bienvenido a nuestro equipo de la licorería.";
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
                    }
                }
            }

            TempData["ErrorMessage"] = "¡Ups! Hubo un problema al crear el usuario. Por favor, verifica los datos.";
            return View(user);
        }




        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
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

        //// POST: Users/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, ApplicationUser user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var existingUser = await _userManager.FindByIdAsync(id);
        //        if (existingUser == null)
        //        {
        //            return NotFound();
        //        }

        //        existingUser.FullName = user.FullName;
        //        existingUser.Position = user.Position;
        //        existingUser.UserName = user.UserName;
        //        existingUser.Email = user.Email;

        //        var result = await _userManager.UpdateAsync(existingUser);
        //        if (result.Succeeded)
        //        {
        //            TempData["SuccessMessage"] = "¡Usuario actualizado con éxito!";
        //            return RedirectToAction(nameof(Index));
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }
        //    TempData["ErrorMessage"] = "¡Ups! No se pudo actualizar el usuario. Inténtalo de nuevo.";
        //    return View(user);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(user);
                if (result)
                {
                    TempData["SuccessMessage"] = "¡Usuario actualizado con éxito!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el usuario.");
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

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Username, FullName = model.Username, Position = "Vendedor" };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            TempData["SuccessMessage"] = "¡Registro exitoso! Bienvenido a la familia de la licorería.";
        //            return RedirectToAction("Index", "Home");
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }
        //    TempData["ErrorMessage"] = "No se pudo completar el registro. Por favor, verifica los datos.";
        //    return View(model);
        //}


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

