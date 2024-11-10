using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // DbSet para Products
        public DbSet<Products> Products { get; set; }

        // DbSet para Sales
        public DbSet<Sales> Sales { get; set; }

        // DbSet para User
        public DbSet<User> Users { get; set; } // Cambié 'Users' a 'User'
    }
}


