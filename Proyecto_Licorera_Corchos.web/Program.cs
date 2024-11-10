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

// Configuración del entorno y asi 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Agregar autenticación
app.UseAuthentication();
app.UseAuthorization();

// Configurar roles y usuarios iniciales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        SeedRolesAndUsersAsync(roleManager, userManager).Wait(); // Importante: Usamos .Wait() para ejecutar la tarea de forma síncrona
    }
    catch (Exception ex)
    {
        // Maneja los errores de inicialización aquí
        Console.WriteLine($"Error initializing roles and users: {ex.Message}");
    }
}

// Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id_Sales?}");

// Personalizar la configuración de la aplicación lo puede hacer kellys tu tesa
app.AddCustomwebAppConfiguration();

app.Run();

// Método para crear roles y usuarios iniciales
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

    // Crear un usuario administrador si no existe
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin",
            FullName = "Administrador Principal",
            Position = "Admin"
        };
        var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Cambia la contraseña por una segura
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

