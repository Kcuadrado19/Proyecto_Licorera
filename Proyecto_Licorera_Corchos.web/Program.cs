using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.Data.Entities;
using Proyecto_Licorera_Corchos.web.RoleManagement;
using Proyecto_Licorera_Corchos.web.Services;

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
    options.LoginPath = "/Users/Login";
    options.AccessDeniedPath = "/Users/AccessDenied";
});

// Agregar RoleService
builder.Services.AddScoped<IRoleService, RoleService>();

// Agregar servicios para controladores y vistas
builder.Services.AddControllersWithViews();

// Personalizar la configuración
builder.AddCustomBuilderConfiguration();

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

// Configurar roles, permisos y usuarios iniciales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<DataContext>();

        SeedRolesAndUsersAsync(roleManager, userManager, context).Wait();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing roles, permissions, and users: {ex.Message}");
    }
}

// Configurar rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id_Sales?}");

// Personalizar la configuración de la aplicación
app.AddCustomwebAppConfiguration();

app.Run();

// Método para crear roles, permisos y usuarios iniciales
async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, DataContext context)
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

    // Crear usuarios administradores
    var admin1 = await userManager.FindByNameAsync("lucia.admin");
    if (admin1 == null)
    {
        admin1 = new ApplicationUser
        {
            UserName = "lucia.admin",
            FullName = "Lucía Fernández",
            Position = "Admin"
        };
        var result = await userManager.CreateAsync(admin1, "LuciaAdmin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin1, "Admin");
        }
    }

    var admin2 = await userManager.FindByNameAsync("carlos.admin");
    if (admin2 == null)
    {
        admin2 = new ApplicationUser
        {
            UserName = "carlos.admin",
            FullName = "Carlos Ramírez",
            Position = "Admin"
        };
        var result = await userManager.CreateAsync(admin2, "CarlosAdmin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin2, "Admin");
        }
    }

    // Crear usuarios vendedores
    var vendedor1 = await userManager.FindByNameAsync("maria.vendedor");
    if (vendedor1 == null)
    {
        vendedor1 = new ApplicationUser
        {
            UserName = "maria.vendedor",
            FullName = "María López",
            Position = "Vendedor"
        };
        var result = await userManager.CreateAsync(vendedor1, "MariaVendedor@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(vendedor1, "Vendedor");
        }
    }

    var vendedor2 = await userManager.FindByNameAsync("pedro.vendedor");
    if (vendedor2 == null)
    {
        vendedor2 = new ApplicationUser
        {
            UserName = "pedro.vendedor",
            FullName = "Pedro Martínez",
            Position = "Vendedor"
        };
        var result = await userManager.CreateAsync(vendedor2, "PedroVendedor@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(vendedor2, "Vendedor");
        }
    }
}






