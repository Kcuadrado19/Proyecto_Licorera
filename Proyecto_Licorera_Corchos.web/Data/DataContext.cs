using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using static Proyecto_Licorera_Corchos.web.Data.Entities.IdentityUserToken;

namespace Proyecto_Licorera_Corchos.web.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // DbSet para tus otras entidades
        public DbSet<Product> Product { get; set; }
        public DbSet<Sales> Sales { get; set; }

        // DbSet para la nueva entidad RolePermission y Permission
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<LicoreraRole> LicoreraRoles { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<RoleSection> RoleSections { get; set; }

        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<ApplicationUserToken> UserTokens { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureKeys(modelBuilder);
            ConfigureIndexes(modelBuilder);

        }

        private void ConfigureKeys(ModelBuilder modelBuilder) 
        {
            // Configurar la relación muchos a muchos entre IdentityRole y Permission
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.RolePermission) // Propiedad de navegación en ApplicationUser
                .WithMany(rp => rp.Users)      // Propiedad de navegación en RolePermission
                .HasForeignKey(u => new { u.RolePermissionRoleId, u.RolePermissionPermissionId })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUserRole>()
                 .HasOne(ur => ur.User)
                 .WithMany(u => u.UserRoles)
                 .HasForeignKey(ur => ur.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUserToken>()
                 .HasOne(ut => ut.User)
                 .WithMany(u => u.UserTokens)
                 .HasForeignKey(ut => ut.UserId)
                 .OnDelete(DeleteBehavior.Restrict);


            // Role Sections
            modelBuilder.Entity<RoleSection>()
                 .HasKey(rs => new { rs.RoleId, rs.SectionId });

            modelBuilder.Entity<RoleSection>()
                .HasOne(rs => rs.Role)
                .WithMany(r => r.RoleSections)
                .HasForeignKey(rs => rs.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoleSection>()
                .HasOne(rs => rs.Section)
                .WithMany(s => s.RoleSections)
                .HasForeignKey(rs => rs.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        private void ConfigureIndexes(ModelBuilder modelBuilder) 
        {
            // Índices únicos para nombres
            modelBuilder.Entity<Section>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Permission>()
               .HasIndex(p => p.Name)
               .IsUnique();


        }



    }
}


