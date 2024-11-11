using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.RoleManagement; // Importa RoleManagement para usar RoleService
using Proyecto_Licorera_Corchos.web.Services; // Importa el espacio de nombres para IRoleService

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// lady: Configurar la cadena de conexión
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// lady: Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// lady: Configurar cookies de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Users/Login"; // lady: Ruta para el inicio de sesión
    options.AccessDeniedPath = "/Users/AccessDenied"; // lady: Ruta para el acceso denegado
});

// lady: Agregar RoleService
builder.Services.AddScoped<IRoleService, RoleService>(); // Registro de RoleService corregido

// lady: Agregar servicios para controladores y vistas
builder.Services.AddControllersWithViews();

// lady: Personalizar la configuración
builder.AddCustomBuilderConfiguration(); // lady: parametrización por referencia en clase Customconfiguration

WebApplication app = builder.Build();

// lady: Configuración del entorno
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// lady: Configurar roles, permisos y usuario administrador inicial
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<DataContext>();

        SeedRolesAndUsersAsync(roleManager, userManager, context).Wait(); // lady: Ejecutar de forma síncrona
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing roles, permissions, and users: {ex.Message}"); // lady: Manejar errores
    }
}

// lady: Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id_Sales?}");

// lady: Personalizar la configuración de la aplicación
app.AddCustomwebAppConfiguration();

app.Run();

// lady: Método para crear roles, permisos y usuario administrador inicial
async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, DataContext context)
{
    // lady: Crear roles si no existen
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    if (!await roleManager.RoleExistsAsync("Vendedor"))
    {
        await roleManager.CreateAsync(new IdentityRole("Vendedor"));
    }

    // lady: Definir permisos de ejemplo
    var permissions = new List<Permission>
    {
        new Permission { Name = "Crear Ventas", Description = "Permite crear nuevas ventas" },
        new Permission { Name = "Eliminar Ventas", Description = "Permite eliminar ventas existentes" },
        new Permission { Name = "Ver Productos", Description = "Permite ver el listado de productos" },
        new Permission { Name = "Gestionar Usuarios", Description = "Permite gestionar los usuarios de la aplicación" }
    };

    // lady: Agregar los permisos a la base de datos si no existen
    foreach (var permission in permissions)
    {
        if (!context.Permissions.Any(p => p.Name == permission.Name))
        {
            context.Permissions.Add(permission);
        }
    }
    await context.SaveChangesAsync();

    // lady: Asignar permisos a los roles
    var adminRole = await roleManager.FindByNameAsync("Admin");
    var vendedorRole = await roleManager.FindByNameAsync("Vendedor");

    // lady: Asignar todos los permisos al rol Admin
    foreach (var permission in permissions)
    {
        if (!context.RolePermissions.Any(rp => rp.RoleId == adminRole.Id && rp.PermissionId == permission.Id))
        {
            context.RolePermissions.Add(new RolePermission { RoleId = adminRole.Id, PermissionId = permission.Id });
        }
    }

    // lady: Asignar permisos específicos al rol Vendedor
    var vendedorPermissions = permissions.Where(p => p.Name == "Crear Ventas" || p.Name == "Ver Productos");
    foreach (var permission in vendedorPermissions)
    {
        if (!context.RolePermissions.Any(rp => rp.RoleId == vendedorRole.Id && rp.PermissionId == permission.Id))
        {
            context.RolePermissions.Add(new RolePermission { RoleId = vendedorRole.Id, PermissionId = permission.Id });
        }
    }
    await context.SaveChangesAsync();

    // lady: Crear un usuario administrador inicial si no existe
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin",
            FullName = "Administrador Principal",
            Position = "Admin"
        };
        var result = await userManager.CreateAsync(adminUser, "Admin@123"); // lady: Cambia esta contraseña por una segura
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}





