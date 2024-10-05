using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }

        public DbSet<test1> test1 { get; set; }
    }
}
