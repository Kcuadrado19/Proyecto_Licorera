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

        Task<List<ApplicationUser>> GetAllUsersAsync();


        Task<bool> CreateUserAsync(ApplicationUser user, string password);


        Task<ApplicationUser> GetUserByIdAsync(string userId);

        public Task<ApplicationUser> GetUserAsync(string email);

        Task<Response<PaginationResponse<ApplicationUser>>> GetlistAsync(PaginationRequest request);


        Task<bool> UpdateUserAsync(ApplicationUser user);


        Task<bool> DeleteUserAsync(string userId);

        public Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module);


        Task<bool> SignInUserAsync(string username, string password, bool rememberMe);

        public Task<SignInResult> LoginAsync(LoginDTO dto);
        public Task LogoutAsync();

        Task SignOutUserAsync();

        Task<bool> RegisterUserAsync(ApplicationUser user, string password);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _context;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }


        public async Task<ApplicationUser> GetUserAsync(string email)
        {
            ApplicationUser? user = await _context.Users.Include(u => u.RolePermission)
                                             .FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
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


        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
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



        public async Task<SignInResult> LoginAsync(LoginDTO dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
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


        public async Task<bool> RegisterUserAsync(ApplicationUser user, string password)
        {
            user.UserName = user.Email; //  Asegura que UserName se asigna correctamente
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result.Succeeded;
        }

        public async Task<Response<PaginationResponse<ApplicationUser>>> GetlistAsync(PaginationRequest request)
        {

            try
            {
                IQueryable<ApplicationUser> query = _userManager.Users.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.FullName.ToLower().Contains(request.Filter.ToLower()) || s.Email.ToLower().Contains(request.Filter.ToLower()));
                }


                PagedList<ApplicationUser> List = await PagedList<ApplicationUser>.ToPagedListAsync(query, request);

                PaginationResponse<ApplicationUser> result = new PaginationResponse<ApplicationUser>
                {
                    List = List,
                    TotalCount = List.TotalCount,
                    RecordsPerPage = List.RecordsPerPage,
                    CurrentPage = List.CurrentPage,
                    TotalPages = List.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<ApplicationUser>>.MakeResponseSuccess(result, "Producto obtenido con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<ApplicationUser>>.MakeResponseFail(ex);
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


