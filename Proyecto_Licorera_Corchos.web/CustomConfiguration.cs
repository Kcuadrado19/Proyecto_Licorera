using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;
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


        public static void AddServices( WebApplicationBuilder builder)

        {
            builder.Services.AddScoped<ISectionService,SectionService > ();
            builder.Services.AddScoped<ISalesService, SalesService>();
        }

        public static WebApplication AddCustomWebAppConfiguration(this WebApplication app) 
        {
            app.UseNotyf();
            return app;
        }

        public static WebApplication AddCustomwebAppConfiguration(this WebApplication app)
        {
            app.UseNotyf();
            return app;
        }

    }
}
