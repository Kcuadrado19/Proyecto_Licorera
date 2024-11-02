using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface ISectionService
    {

       public Task<Response<Section>> CreateAsync(Section model);

        public Task<Response<Section>> EditAsync(Section model);
       public Task<Response<List<Section>>> GetlistAsync();
       public Task<Response<Section>> GetOneAsync( int id);



    }

    public class SectionService : ISectionService
    {
        private readonly DataContext _context;

    public SectionService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Section>> CreateAsync(Section model)
        {
            try
            {
                Section section = new Section
                {
                    Name = model.Name,
                };

                await _context.Section.AddAsync(section);
                await _context.SaveChangesAsync();

                return ResponseHelper<Section>.MakeResponseSuccess(section, "Sección creada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Section>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<Section>> EditAsync(Section model)
        {

            try
            {
                

                 _context.Section.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Section>.MakeResponseSuccess(model, "Sección creada con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Section>.MakeResposeFail(ex);
            }
        }

        public async Task<Response<List<Section>>> GetlistAsync()
        {
            try
            {
                List<Section> sections = await _context.Section.ToListAsync();

                return ResponseHelper<List<Section>>.MakeResponseSuccess (sections);

            }
            catch (Exception ex) 
            { 
                return ResponseHelper<List<Section>>.MakeResposeFail(ex);
            }


        }

        public async Task<Response<Section>> GetOneAsync(int id)
        {
            try
            {
                Section? section = await _context.Section.FirstOrDefaultAsync(s => s.Id == id);

                if (section is null)
                {
                    return ResponseHelper<Section>.MakeResposeFail("La sección con el id indicado no existe");
                }

                return ResponseHelper<Section>.MakeResponseSuccess(section);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Section>.MakeResposeFail(ex);
            }
        }
    }
}
