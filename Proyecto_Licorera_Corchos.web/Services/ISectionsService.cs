
using AspNetCoreHero.ToastNotification.Abstractions;
using Azure.Core;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Helpers;

namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface ISectionsService
    {
        //public Task<Response<List<Section>>> GetListAsync();
        public Task<Response<PaginationResponse<Section>>> GetListAsync(PaginationRequest request);

    }

    public class SectionsService : ISectionsService
    {
        private readonly DataContext _context;
       

        public SectionsService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginationResponse<Section>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Section> query = _context.Sections.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Section> list = await PagedList<Section>.ToPagedListAsync(query, request);

                PaginationResponse<Section> result = new PaginationResponse<Section>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<Section>>.MakeResponseSuccess(result, "Secciones obtenidas con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Section>>.MakeResposeFail(ex);
            }
        }
    }
}