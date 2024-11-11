using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// Configurar cookies de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Users/Login"; // Ruta para el inicio de sesión
    options.AccessDeniedPath = "/Users/AccessDenied"; // Ruta para el acceso denegado
});

// Agregar servicios para controladores y vistas
builder.Services.AddControllersWithViews();

// Personalizar la configuración
builder.AddCustomBuilderConfiguration(); // parametrización por referencia en clase Customconfiguration

WebApplication app = builder.Build();

// Configuración del entorno
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

// Configurar roles y usuario administrador inicial
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        SeedRolesAndUsersAsync(roleManager, userManager).Wait(); // Ejecutar de forma síncrona
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing roles and users: {ex.Message}");
    }
}


// Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id_Sales?}");

// Personalizar la configuración de la aplicación
app.AddCustomwebAppConfiguration();

app.Run();

// Método para crear roles y usuario administrador inicial
async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    // Crear roles si no existen
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    if (!await roleManager.RoleExistsAsync("Vendedor"))
    {
        await roleManager.CreateAsync(new IdentityRole("Vendedor"));
    }

    // Crear un usuario administrador inicial si no existe
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin",
            FullName = "Administrador Principal",
            Position = "Admin"
        };
        var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Cambia esta contraseña por una segura
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}


