using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
using Proyecto_Licorera_Corchos.web.RoleManagement;
using Proyecto_Licorera_Corchos.web.Services;

namespace Proyecto_Licorera_Corchos.web
{

    //método de extensión, es estatico
    //this - parametrización por referencia
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            //aca se hacen las modificaciones al builder

            //data context

            builder.Services.AddDbContext<DataContext>(configuration =>
            {
                configuration.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            //Services

            AddServices(builder);

            //toast notification
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            return builder;
        }


        public static void AddServices(WebApplicationBuilder builder)

        {
            builder.Services.AddScoped<ISalesService, SalesService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISectionsService, SectionsService>();
        }

        public static WebApplication AddCustomWebAppConfiguration(this WebApplication app)
        {
            app.UseNotyf();
            return app;
        }


    }
}
//private static void AddIAM(WebApplicationBuilder builder)
//{
//    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(conf =>
//    {
//        conf.User.RequireUniqueEmail = true;
//        conf.Password.RequireDigit = false;
//        conf.Password.RequiredUniqueChars = 0;
//        conf.Password.RequireLowercase = false;
//        conf.Password.RequireUppercase = false;
//        conf.Password.RequireNonAlphanumeric = false;
//        conf.Password.RequiredLength = 4;
//    }).AddEntityFrameworkStores<DataContext>()
//      .AddDefaultTokenProviders();

//    builder.Services.ConfigureApplicationCookie(conf =>
//    {
//        conf.Cookie.Name = "Auth";
//        conf.ExpireTimeSpan = TimeSpan.FromDays(100);
//        conf.LoginPath = "/Account/Login";
//        conf.AccessDeniedPath = "/Account/NotAuthorized";
//    });
//}


