using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web;
using Proyecto_Licorera_Corchos.web.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.AddCustomBuilderConfiguration();   //this - parametrización por referencia en clase Customconfiguration

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
    pattern: "{controller=Home}/{action=Index}/{Id_Sales?}");


app.AddCustomwebAppConfiguration();

app.Run();