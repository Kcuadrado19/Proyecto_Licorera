using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Helpers;
namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface ISalesService
    {
        public Task<Response<Sales>> CreateAsync(Sales model);

        public Task<Response<Sales>> DeleteAsync(int Id_Sales);
        public Task<Response<Sales>> EditAsync(Sales model);
        public Task<Response<List<Sales>>> GetlistAsync();
        public Task<Response<Sales>> GetOneAsync(int Id_Sales );
        
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
                    Sale_Date =model.Sale_Date,
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

        public  async Task<Response<List<Sales>>> GetlistAsync()
        {
            try
            {
                List<Sales> sales1 = await _context.Sales.ToListAsync();

                return ResponseHelper<List<Sales>>.MakeResponseSuccess(sales1);

            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Sales>>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Sales>> GetOneAsync(int Id_Sales)
        {
            try
            {
                Sales? sales1 = await _context.Sales.FirstOrDefaultAsync(s => s.Id_Sales == Id_Sales);

                if(sales1 is null) 
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

        public async Task<Response<Sales>>DeleteAsync(int Id_Sales)
        {
            try
            {
                Response<Sales> response= await GetOneAsync(Id_Sales);

                if (response.IsSuccess)
                {
                    return response;
                }
                _context.Sales.Remove(response.Result);
                await _context.SaveChangesAsync();

                return ResponseHelper<Sales>.MakeResponseSuccess(null, "Sección eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Sales>.MakeResposeFail(ex);
            }

        }
    }



}
