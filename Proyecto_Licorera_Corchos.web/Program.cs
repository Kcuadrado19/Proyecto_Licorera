using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

//Data Context

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

WebApplication app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
  
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=test1}/{action=Index}/{id?}");

app.Run();
