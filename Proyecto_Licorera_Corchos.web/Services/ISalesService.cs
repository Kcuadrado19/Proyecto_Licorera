using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using Proyecto_Licorera_Corchos.web.Core.Pagination;

namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface ISalesService
    {
        Task<Response<Sales>> CreateAsync(Sales model);
        Task<Response<Sales>> DeleteAsync(int Id_Sales);
        Task<Response<Sales>> EditAsync(Sales model);
        Task<Response<PaginationResponse<Sales>>> GetlistAsync(PaginationRequest request);
        Task<Response<Sales>> GetOneAsync(int Id_Sales);
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
                    ProductId = model.ProductId,
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

                return ResponseHelper<Sales>.MakeResponseSuccess(model, "Venta actualizada con éxito");
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
                //IQueryable<Sales> query = _context.Sales.AsQueryable();
                IQueryable<Sales> query = _context.Sales
                        .Include(s => s.Product);

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    //query = query.Where(s => s.Product.Name != null && s.Product.Name.ToLower().Contains(request.Filter.ToLower()));
                    query = query.Where(s => s.Product.Name.Contains(request.Filter));

                }

                // Aquí se corrige el uso de PaginatedList en lugar de PagedList
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

        public async Task<Response<Sales>> GetOneAsync(int Id_Sales)
        {
            try
            {
                var sales = await _context.Sales
                    .Include(s => s.Product) // Carga los datos del producto relacionado
                    .FirstOrDefaultAsync(s => s.Id_Sales == Id_Sales);

                if (sales == null)
                {
                    return ResponseHelper<Sales>.MakeResposeFail("La venta con el id indicado no existe");
                }
                return ResponseHelper<Sales>.MakeResponseSuccess(sales);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sales>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Sales>> DeleteAsync(int Id_Sales)
        {
            try
            {
                Response<Sales> response = await GetOneAsync(Id_Sales);

                if (!response.IsSuccess)
                {
                    return response;
                }
                _context.Sales.Remove(response.Result);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sales>.MakeResponseSuccess(null, "Venta eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sales>.MakeResposeFail(ex);
            }
        }
    }
}