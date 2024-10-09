using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data.Entities;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }


        public DbSet<Clientes> Clientes { get; set; }

        public DbSet<Productos> Productos { get; set; }

        public DbSet<Ventas> Ventas { get; set; }
        
        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<Ausuarios> Ausuarios { get; set; }

        public DbSet<Accounting> Accounting { get; set; }

        public DbSet<Modificaciones> Modificaciones { get; set; }

        public DbSet<Permisos> Permisos { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
