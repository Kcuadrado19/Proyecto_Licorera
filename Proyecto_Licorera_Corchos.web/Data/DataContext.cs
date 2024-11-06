using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }

        public DbSet<Products> Products { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<Users> Users { get; set; }
      
    
    }
}
