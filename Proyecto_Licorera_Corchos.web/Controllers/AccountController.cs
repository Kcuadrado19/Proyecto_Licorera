using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Core.DTOs;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Services;

namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class AccountController : Controller
    {

        private readonly INotyfService _notifyService;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IUserService usersService, INotyfService notifyService, SignInManager<User> signInManager)
        {
            _userService = usersService;
            _notifyService = notifyService;
            _signInManager = signInManager;
        }

        //[HttpGet]
        //public ActionResult Login()
        //{
        //    //return View();
        //    return View(new LoginDTO());
        //}

        [HttpGet]
        public IActionResult Login()
        {
            // Crear un LoginViewModel (si lo estás usando)


            // Convertir LoginViewModel a LoginDTO
    
            };

            return View(loginDto);  // Pasar el LoginDTO a la vista
        }




        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userService.LoginAsync(dto);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                _notifyService.Error("Email o contraseña incorrectos");

                return View(dto);
            }

            return View(dto);
        }



        

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Usamos _signInManager para autenticar al usuario con las credenciales
        //        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home"); // Redirige a la página principal
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
        //            _notifyService.Error("Email o contraseña incorrectos");
        //        }
        //    }

        //    return View(model); // Si hay errores de validación, regresa el modelo a la vista
        //}



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
