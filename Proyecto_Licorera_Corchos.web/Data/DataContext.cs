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
    }
}
