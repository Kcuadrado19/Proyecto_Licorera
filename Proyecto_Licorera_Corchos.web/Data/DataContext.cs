using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.Models;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // DbSet para tus otras entidades
        public DbSet<Products> Products { get; set; }
        public DbSet<Sales> Sales { get; set; }
    }
}



