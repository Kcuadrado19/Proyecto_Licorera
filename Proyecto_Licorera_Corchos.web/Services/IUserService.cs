using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.DTOs;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface IUserService
    {

        Task<List<User>> GetAllUsersAsync();


        Task<bool> CreateUserAsync(User user, string password);


        Task<User> GetUserByIdAsync(string userId);

        public Task<User> GetUserAsync(string email);

        Task<Response<PaginationResponse<User>>> GetlistAsync(PaginationRequest request);


        Task<bool> UpdateUserAsync(User user);


        Task<bool> DeleteUserAsync(string userId);

        public Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module);


        Task<bool> SignInUserAsync(string username, string password, bool rememberMe);

        public Task<SignInResult> LoginAsync(LoginDTO loginDTO);
        public Task LogoutAsync();

        Task SignOutUserAsync();

        Task<bool> RegisterUserAsync(User user, string password);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }


        public async Task<User> GetUserAsync(string email)
        {
            User? user = await _context.Users.Include(u => u.RolePermission)
                                             .FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {
            if (user == null || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // Verificar si ya existe un usuario con el mismo email
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                Console.WriteLine("Ya existe un usuario con este correo electrónico.");
                return false;
            }

            user.UserName = user.Email; // Asegúrate de establecer el nombre de usuario correctamente
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error creando el usuario: {error.Description}");
                }
            }

            return result.Succeeded;
        }


        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                Console.WriteLine("Usuario no encontrado para actualización.");
                return false;
            }

            // Actualiza solo las propiedades generales del usuario, excluyendo la contraseña
            existingUser.FullName = user.FullName;
            existingUser.Position = user.Position;
            existingUser.Email = user.Email;
            existingUser.UserName = user.Email; // Mantenemos UserName igual al email

            // Realiza la actualización del usuario en la base de datos
            var updateResult = await _userManager.UpdateAsync(existingUser);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    Console.WriteLine($"Error al actualizar el usuario: {error.Description}");
                }
                return false;
            }

            Console.WriteLine("Usuario actualizado exitosamente en la base de datos.");
            return true;
        }



        public async Task<SignInResult> LoginAsync(LoginDTO loginDTO)
        {
            return await _signInManager.PasswordSignInAsync(
                loginDTO.Email,
                loginDTO.Password,
                loginDTO.RememberMe,
                lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> SignInUserAsync(string username, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, false);
            return result.Succeeded;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<bool> RegisterUserAsync(User user, string password)
        {
            user.UserName = user.Email; //  Asegura que UserName se asigna correctamente
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result.Succeeded;
        }

        public async Task<Response<PaginationResponse<User>>> GetlistAsync(PaginationRequest request)
        {

            try
            {
                IQueryable<User> query = _userManager.Users.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.FullName.ToLower().Contains(request.Filter.ToLower()) || s.Email.ToLower().Contains(request.Filter.ToLower()));
                }


                PagedList<User> List = await PagedList<User>.ToPagedListAsync(query, request);

                PaginationResponse<User> result = new PaginationResponse<User>
                {
                    List = List,
                    TotalCount = List.TotalCount,
                    RecordsPerPage = List.RecordsPerPage,
                    CurrentPage = List.CurrentPage,
                    TotalPages = List.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<User>>.MakeResponseSuccess(result, "Producto obtenido con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<User>>.MakeResponseFail(ex);
            }


        }

        public async Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module)
        {
            // Obtén el usuario actual basado en el contexto
            var user = await _userManager.GetUserAsync(_signInManager.Context.User);
            if (user == null)
            {
                return false; // Si no hay un usuario autenticado, no está autorizado
            }

            // Verifica si el usuario tiene los permisos necesarios
            var userPermissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == user.RoleId && rp.Permission.Module == module && rp.Permission.Name == permission)
                .ToListAsync();

            // Si encuentra al menos un permiso válido, el usuario está autorizado
            return userPermissions.Any();
        }



    }
}


