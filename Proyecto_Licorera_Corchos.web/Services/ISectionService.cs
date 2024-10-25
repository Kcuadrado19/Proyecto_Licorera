using Proyecto_Licorera_Corchos.web.Core;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Licorera_Corchos.web.Services
{
    public interface ISectionService
    {

        public Task<Response<List<Section>>> GetlistAsync();
    
    }

    public class SectionService : ISectionService
    {
        private readonly DataContext _context;
    public SectionService(DataContext context)
        {
            _context = context;
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
    }
}
