using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface ISalesService
    {
        public Task<Response<Sales>> CreateAsync(Sales model);

        public Task<Response<Sales>> DeleteAsync(int Id);
        public Task<Response<Sales>> EditAsync(Sales model);
        public Task<Response<PaginationResponse<Sales>>> GetlistAsync(PaginationRequest request);
        public Task<Response<Sales>> GetOneAsync(int Id );
        
    }

    public class SalesService : ISalesService
    {
        private readonly DataContext _context;

        public SalesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Sales>> CreateAsync(Sales model)
        {
            try
            {
                Sales sales1 = new Sales
                {
                    Name = model.Name,
                    Sale_Date = model.Sale_Date,
                    Sales_Value = model.Sales_Value,

                };

                await _context.Sales.AddAsync(sales1);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sales>.MakeResponseSuccess(sales1, "Nueva venta registrada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sales>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Sales>> EditAsync(Sales model)
        {
            try
            {
                _context.Sales.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sales>.MakeResponseSuccess(model, "Nueva venta actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sales>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<Sales>>> GetlistAsync(PaginationRequest request)
        {
            try
            {
              IQueryable<Sales> query = _context.Sales.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter)) 
                {
                    query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Sales> List = await PagedList<Sales>.ToPagedListAsync(query, request);

                PaginationResponse<Sales> result = new PaginationResponse<Sales>
                {
                    List = List,
                    TotalCount = List.TotalCount,
                    RecordsPerPage = List.RecordsPerPage,
                    CurrentPage = List.CurrentPage,
                    TotalPages = List.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<Sales>>.MakeResponseSuccess(result, "Ventas obtenidas con éxito");

            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Sales>>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Sales>> GetOneAsync(int Id)
        {
            try
            {
                Sales? sales1 = await _context.Sales.FirstOrDefaultAsync(s => s.Id == Id);

                if (sales1 is null)
                {
                    return ResponseHelper<Sales>.MakeResposeFail("La venta con el id indicado no existe");
                }
                return ResponseHelper<Sales>.MakeResponseSuccess(sales1);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sales>.MakeResposeFail(ex);
            }
        }


       
        public async Task<Response<Sales>>DeleteAsync(int Id)
        {
            try
            {
                Response<Sales> response= await GetOneAsync(Id);

                if (!response.IsSuccess)
                {
                    return response;
                }
                _context.Sales.Remove(response.Result);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sales>.MakeResponseSuccess(null, "venta eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sales>.MakeResposeFail(ex);
            }

        }
    }



}
