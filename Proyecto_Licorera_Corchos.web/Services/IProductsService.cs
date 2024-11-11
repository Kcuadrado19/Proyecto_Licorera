using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Core;
using System.Threading.Tasks;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Licorera_Corchos.web.Core.Pagination;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;


namespace Proyecto_Licorera_Corchos.web.Services
{


    public interface IProductService
    {
        public Task<Response<PaginationResponse<Product>>> GetlistAsync(PaginationRequest request);
        public Task<Response<Product>> CreateAsync(Product model);
        public Task<Response<Product>> EditAsync(Product model);
        public Task<Response<Product>> DeleteAsync(int id);
        public Task<Response<Product>> GetOneAsync(int id);
        


    }
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Product>> CreateAsync(Product model)
        {
            try
            {
                Product product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,

                };

                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();

                return ResponseHelper<Product>.MakeResponseSuccess(product, "Nuevo producto registrado con éxito");
            }
            catch (Exception ex)
            {
                
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Product>> EditAsync(Product model)
        {
            try
            {
                _context.Product.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Product>.MakeResponseSuccess(model, "Nueva venta actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Product>> DeleteAsync(int Id)
        {
            try
            {
                var product = await _context.Product.FindAsync(Id);
                if (product == null)
                {
                    return ResponseHelper<Product>.MakeResposeFail("Producto no encontrado.");
                }

                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                return ResponseHelper<Product>.MakeResponseSuccess(null, "Producto eliminado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<Product>>> GetlistAsync(PaginationRequest request)
        {

            try
            {
                IQueryable<Product> query = _context.Product.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Product> List = await PagedList<Product>.ToPagedListAsync(query, request);

                PaginationResponse<Product> result = new PaginationResponse<Product>
                {
                    List = List,
                    TotalCount = List.TotalCount,
                    RecordsPerPage = List.RecordsPerPage,
                    CurrentPage = List.CurrentPage,
                    TotalPages = List.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<Product>>.MakeResponseSuccess(result, "Producto obtenido con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Product>>.MakeResposeFail(ex);
            }


        }

        public async Task<Response<Product>> GetOneAsync(int Id)
        {
            try
            {
                Product? product = await _context.Product.FirstOrDefaultAsync(s => s.Id == Id);

                if (product is null)
                {
                    return ResponseHelper<Product>.MakeResposeFail("El producto con el id indicado no existe");
                }
                return ResponseHelper<Product>.MakeResponseSuccess(product);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }
    }
}

