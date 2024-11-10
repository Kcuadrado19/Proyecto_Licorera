using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Core;
using System.Threading.Tasks;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Licorera_Corchos.web.Services
{


    public interface IProductService
    {
        Task<Response<Product>> CreateAsync(Product product);
        Task<Response<Product>> EditAsync(Product product);
        Task<Response<Product>> DeleteAsync(int id);
        Task<Response<Product>> GetByIdAsync(int id);
        Task<Response<IEnumerable<Product>>> GetAllAsync();
    }
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Product>> CreateAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return ResponseHelper<Product>.MakeResponseSuccess(product, "Producto creado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Product>> EditAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return ResponseHelper<Product>.MakeResponseSuccess(product, "Producto actualizado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Product>> DeleteAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return ResponseHelper<Product>.MakeResposeFail("Producto no encontrado.");
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return ResponseHelper<Product>.MakeResponseSuccess(null, "Producto eliminado con éxito.");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Product>> GetByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return ResponseHelper<Product>.MakeResposeFail("Producto no encontrado.");
                }

                return ResponseHelper<Product>.MakeResponseSuccess(product);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Product>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<IEnumerable<Product>>> GetAllAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return ResponseHelper<IEnumerable<Product>>.MakeResponseSuccess(products);
            }
            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<Product>>.MakeResposeFail(ex);
            }
        }
    }
}

