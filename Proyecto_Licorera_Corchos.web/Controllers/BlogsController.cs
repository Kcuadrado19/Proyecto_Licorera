using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.DTOs;
using Proyecto_Licorera_Corchos.web.Helpers;
using Proyecto_Licorera_Corchos.web.Services;


namespace Proyecto_Licorera_Corchos.web.Controllers
{
    public class BlogsController
    {

       public class BlogsController : Controller
        {
            private readonly IBlogsService _blogsService;
            private readonly ICombosHelper _combosHelper;
            private readonly INotyfService _notifyService;
            private readonly IConverterHelper _converterHelper;

            public BlogsController(IBlogsService blogService, ICombosHelper combosHelper, INotyfService notifyService, IConverterHelper converterHelper)
            {
                _blogsService = blogService;
                _combosHelper = combosHelper;
                _notifyService = notifyService;
                _converterHelper = converterHelper;
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

                Response<PaginationResponse<BlogDTO>> response = await _blogsService.GetListAsync(request);
                return View(response.Result);
            }

            [HttpGet]
            public async Task<IActionResult> Create()
            {
                BlogDTO dto = new BlogDTO
                {
                    Sections = await _combosHelper.GetComboSections(),
                };

                return View(dto);
            }

            [HttpPost]
            public async Task<IActionResult> Create(BlogDTO dto)
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    dto.Sections = await _combosHelper.GetComboSections();
                    return View(dto);
                }

                Response<BlogDTO> response = await _blogsService.CreateAsync(dto);

                if (!response.IsSuccess)
                {
                    _notifyService.Error(response.Message);
                    dto.Sections = await _combosHelper.GetComboSections();
                    return View(dto);
                }

                _notifyService.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            [HttpGet]
            public async Task<IActionResult> Edit([FromRoute] int id)
            {
                Response<BlogDTO> response = await _blogsService.GetOneAsync(id);

                if (response.IsSuccess)
                {
                    BlogDTO dto = await _converterHelper.ToBlogDTO(response.Result);

                    return View(dto);
                }

                _notifyService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            [HttpPost]
            public async Task<IActionResult> Edit(BlogDTO dto)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        _notifyService.Error("Debe ajustar los errores de validación");
                        return View(dto);
                    }

                    Response<BlogDTO> response = await _blogsService.EditAsync(dto);

                    if (response.IsSuccess)
                    {
                        _notifyService.Success(response.Message);
                        return RedirectToAction(nameof(Index));
                    }

                    _notifyService.Error(response.Message);
                    dto.Sections = await _combosHelper.GetComboSections();
                    return View(dto);
                }
                catch (Exception ex)
                {
                    _notifyService.Error(ex.Message);
                    dto.Sections = await _combosHelper.GetComboSections();
                    return View(dto);
                }
            }
        }

    }
}
