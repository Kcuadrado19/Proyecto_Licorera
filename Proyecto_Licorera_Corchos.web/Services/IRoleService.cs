using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.DTOs;
using Proyecto_Licorera_Corchos.web.Helpers;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_Licorera_Corchos.web.Core;

namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface IRoleService
    {
        Task<Response<RoleDTO>> CreateAsync(RoleDTO dto);
        Task<Response<RoleDTO>> EditAsync(RoleDTO dto);
        Task<Response<PaginationResponse<IdentityRole>>> GetListAsync(PaginationRequest request);

        Task<Response<RoleDTO>> GetOneAsync(string id);
        Task<Response<List<PermissionDTO>>> GetPermissionsAsync();
        Task<Response<List<SectionDTO>>> GetSectionsAsync();
    }

    public class RolesService : IRoleService
    {
        private readonly DataContext _context;

        public RolesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<RoleDTO>> CreateAsync(RoleDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var role = new IdentityRole { Name = dto.Name };
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(dto.PermissionIds))
                {
                    var permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                    foreach (var permissionId in permissionIds)
                    {
                        var rolePermission = new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = permissionId
                        };
                        _context.RolePermissions.Add(rolePermission);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.SectionIds))
                {
                    var sectionIds = JsonConvert.DeserializeObject<List<int>>(dto.SectionIds);
                    foreach (var sectionId in sectionIds)
                    {
                        var roleSection = new RoleSection
                        {
                            RoleId = role.Id,
                            SectionId = sectionId
                        };
                        _context.RoleSections.Add(roleSection);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return ResponseHelper<RoleDTO>.MakeResponseSuccess(dto, "Rol creado con éxito.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper<RoleDTO>.MakeResponseFail(ex.Message);
            }
        }

        public async Task<Response<RoleDTO>> EditAsync(RoleDTO dto)
        {
            try
            {
                var existingPermissions = await _context.RolePermissions
                    .Where(rp => rp.RoleId == dto.Id)
                    .ToListAsync();
                _context.RolePermissions.RemoveRange(existingPermissions);

                if (!string.IsNullOrWhiteSpace(dto.PermissionIds))
                {
                    var permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                    foreach (var permissionId in permissionIds)
                    {
                        var rolePermission = new RolePermission
                        {
                            RoleId = dto.Id,
                            PermissionId = permissionId
                        };
                        _context.RolePermissions.Add(rolePermission);
                    }
                }

                var existingSections = await _context.RoleSections
                    .Where(rs => rs.RoleId == dto.Id)
                    .ToListAsync();
                _context.RoleSections.RemoveRange(existingSections);

                if (!string.IsNullOrWhiteSpace(dto.SectionIds))
                {
                    var sectionIds = JsonConvert.DeserializeObject<List<int>>(dto.SectionIds);
                    foreach (var sectionId in sectionIds)
                    {
                        var roleSection = new RoleSection
                        {
                            RoleId = dto.Id,
                            SectionId = sectionId
                        };
                        _context.RoleSections.Add(roleSection);
                    }
                }

                await _context.SaveChangesAsync();
                return ResponseHelper<RoleDTO>.MakeResponseSuccess(dto, "Rol actualizado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper<RoleDTO>.MakeResponseFail(ex.Message);
            }
        }

        public async Task<Response<PaginationResponse<IdentityRole>>> GetListAsync(PaginationRequest request)
        {
            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query = query.Where(r => r.Name.Contains(request.Filter));
            }

            var pagedList = await PagedList<IdentityRole>.ToPagedListAsync(query, request);
            return ResponseHelper<PaginationResponse<IdentityRole>>.MakeResponseSuccess(new PaginationResponse<IdentityRole>
            {
                List = pagedList,
                TotalCount = pagedList.TotalCount,
                RecordsPerPage = pagedList.RecordsPerPage,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                Filter = request.Filter
            }, "Roles obtenidos con éxito.");
        }

        public async Task<Response<RoleDTO>> GetOneAsync(string id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return ResponseHelper<RoleDTO>.MakeResponseFail("Rol no encontrado.");
            }

            var permissions = await GetPermissionsAsync();
            var sections = await GetSectionsAsync();

            return ResponseHelper<RoleDTO>.MakeResponseSuccess(new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = permissions.Result,
                Sections = sections.Result
            });
        }

        public async Task<Response<List<PermissionDTO>>> GetPermissionsAsync()
        {
            var permissions = await _context.Permissions.Select(p => new PermissionDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module
            }).ToListAsync();

            return ResponseHelper<List<PermissionDTO>>.MakeResponseSuccess(permissions);
        }

        public async Task<Response<List<SectionDTO>>> GetSectionsAsync()
        {
            var sections = await _context.Sections.Select(s => new SectionDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                IsHidden = s.IsHidden
            }).ToListAsync();

            return ResponseHelper<List<SectionDTO>>.MakeResponseSuccess(sections);
        }
    }
}