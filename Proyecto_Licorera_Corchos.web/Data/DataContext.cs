using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }


        public DbSet<Clients> Clientes { get; set; }

        public DbSet<Products> Productos { get; set; }

        public DbSet<Sales> Ventas { get; set; }
        
        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<UsersAudit> Ausuarios { get; set; }

        public DbSet<Accounting> Accounting { get; set; }

        public DbSet<Modifications> Modificaciones { get; set; }

        public DbSet<Permissions> Permisos { get; set; }

        public DbSet<Users> Usuarios { get; set; }
    }
}
