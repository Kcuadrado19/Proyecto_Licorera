using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.DTOs;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Helpers;


namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface IRolesService
    {
        Task<Response<RoleDto>> CreateAsync(RoleDto dto);
        Task<Response<RoleDto>> EditAsync(RoleDto dto);
        Task<Response<PaginationResponse<RoleDto>>> GetListAsync(PaginationRequest request);
        Task<Response<RoleDto>> GetOneAsync(int id);
        Task<Response<List<PermissionDto>>> GetPermissionsAsync();
        Task<Response<List<SectionDto>>> GetSectionsAsync();
    }
    public class RolesService : IRolesService
    {
        private readonly DataContext _context;

        public RolesService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Response<RoleDto>> CreateAsync(RoleDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var role = new LicoreraRole { Name = dto.Name };
                _context.LicoreraRoles.Add(role);
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

                return ResponseHelper<RoleDto>.MakeResponseSuccess(dto, "Rol creado con éxito.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ResponseHelper<RoleDto>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<RoleDto>> EditAsync(RoleDto dto)
        {
            try
            {
                var role = await _context.LicoreraRoles.FindAsync(dto.Id);
                if (role == null)
                {
                    return ResponseHelper<RoleDto>.MakeResponseFail("Rol no encontrado.");
                }

                role.Name = dto.Name;
                _context.LicoreraRoles.Update(role);

                // Actualizar permisos
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

                // Actualizar secciones
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
                return ResponseHelper<RoleDto>.MakeResponseSuccess(dto, "Rol actualizado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper<RoleDto>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<RoleDto>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                var query = _context.LicoreraRoles.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(r => r.Name.Contains(request.Filter));
                }

                var pagedList = await PagedList<LicoreraRole>.ToPagedListAsync(query, request);

                var result = new PaginationResponse<RoleDto>
                {
                    List = (PagedList<RoleDto>)pagedList.Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name ?? string.Empty // Aseguramos que no haya valores nulos
                    }).ToList(),
                    TotalCount = pagedList.TotalCount,
                    RecordsPerPage = pagedList.RecordsPerPage,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<RoleDto>>.MakeResponseSuccess(result, "Roles obtenidos con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<RoleDto>>.MakeResponseFail(ex);
            }
        }


        public async Task<Response<RoleDto>> GetOneAsync(int id)
        {
            var role = await _context.LicoreraRoles.FindAsync(id);
            if (role == null)
            {
                return ResponseHelper<RoleDto>.MakeResponseFail("Rol no encontrado.");
            }

            var permissions = await GetPermissionsAsync();
            var sections = await GetSectionsAsync();

            return ResponseHelper<RoleDto>.MakeResponseSuccess(new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = permissions.Result,
                Sections = sections.Result
            });
        }

        public async Task<Response<List<PermissionDto>>> GetPermissionsAsync()
        {
            var permissions = await _context.Permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module
            }).ToListAsync();

            return ResponseHelper<List<PermissionDto>>.MakeResponseSuccess(permissions);
        }

        public async Task<Response<List<SectionDto>>> GetSectionsAsync()
        {
            var sections = await _context.Sections.Select(s => new SectionDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            }).ToListAsync();

            return ResponseHelper<List<SectionDto>>.MakeResponseSuccess(sections);
        }
    }
}
