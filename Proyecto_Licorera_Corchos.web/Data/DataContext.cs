using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }

        public DbSet<Clients> Clients { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Sales> Sales { get; set; }
        
        public DbSet<Orders> Orders { get; set; }

        public DbSet<UsersAudit>  UsersAudit { get; set; }

        public DbSet<Accounting> Accounting { get; set; }

        public DbSet<Modifications> Modifications { get; set; }

        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<Users> Users { get; set; }
    }
}
