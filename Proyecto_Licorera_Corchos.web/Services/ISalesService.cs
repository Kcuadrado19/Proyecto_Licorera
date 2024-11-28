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
        Task<Response<Sale>> CreateAsync(Sale model);
        Task<Response<Sale>> DeleteAsync(int Id_Sales);
        Task<Response<Sale>> EditAsync(Sale model);
        Task<Response<PaginationResponse<Sale>>> GetlistAsync(PaginationRequest request);
        Task<Response<Sale>> GetOneAsync(int Id_Sales);
    }

    public class SalesService : ISalesService
    {
        private readonly DataContext _context;

        public SalesService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Sale>> CreateAsync(Sale model)
        {
            try
            {
                Sale sales1 = new Sale
                {
                    ProductId = model.ProductId,
                    Sale_Date = model.Sale_Date,
                    Sales_Value = model.Sales_Value,
                };

                await _context.Sales.AddAsync(sales1);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sale>.MakeResponseSuccess(sales1, "Nueva venta registrada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sale>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Sale>> EditAsync(Sale model)
        {
            try
            {
                _context.Sales.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sale>.MakeResponseSuccess(model, "Venta actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sale>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<Sale>>> GetlistAsync(PaginationRequest request)
        {
            try
            {
                //IQueryable<Sales> query = _context.Sales.AsQueryable();
                IQueryable<Sale> query = _context.Sales
                        .Include(s => s.Product);

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    //query = query.Where(s => s.Product.Name != null && s.Product.Name.ToLower().Contains(request.Filter.ToLower()));
                    query = query.Where(s => s.Product.Name.Contains(request.Filter));

                }

                // Aquí se corrige el uso de PaginatedList en lugar de PagedList
                PagedList<Sale> List = await PagedList<Sale>.ToPagedListAsync(query, request);

                PaginationResponse<Sale> result = new PaginationResponse<Sale>
                {
                    List = List,
                    TotalCount = List.TotalCount,
                    RecordsPerPage = List.RecordsPerPage,
                    CurrentPage = List.CurrentPage,
                    TotalPages = List.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<Sale>>.MakeResponseSuccess(result, "Ventas obtenidas con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Sale>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Sale>> GetOneAsync(int Id_Sales)
        {
            try
            {
                var sales = await _context.Sales
                    .Include(s => s.Product) // Carga los datos del producto relacionado
                    .FirstOrDefaultAsync(s => s.Id_Sales == Id_Sales);

                if (sales == null)
                {
                    return ResponseHelper<Sale>.MakeResponseFail("La venta con el id indicado no existe");
                }
                return ResponseHelper<Sale>.MakeResponseSuccess(sales);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sale>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Sale>> DeleteAsync(int Id_Sales)
        {
            try
            {
                Response<Sale> response = await GetOneAsync(Id_Sales);

                if (!response.IsSuccess)
                {
                    return response;
                }
                _context.Sales.Remove(response.Result);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sale>.MakeResponseSuccess(null, "Venta eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sale>.MakeResponseFail(ex);
            }
        }
    }
}